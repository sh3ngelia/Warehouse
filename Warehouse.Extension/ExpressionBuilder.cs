using System.Linq.Expressions;
using Warehouse.Extension.Attributes;

namespace Warehouse.Extension;

public static class ExpressionBuilder
{
    public static (string whereQuery, Dictionary<string, object> parameters) Build<T>(this Expression<Func<T, bool>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression, nameof(expression));

        var parameters = new Dictionary<string, object>();
        var whereClause = VisitExpression(expression.Body, parameters);

        return (whereClause, parameters);
    }

    private static string VisitExpression(Expression expressionNode, Dictionary<string, object> parameters)
    {
        return expressionNode switch
        {
            BinaryExpression binaryExpression =>
                VisitBinaryExpression(binaryExpression, parameters),

            MemberExpression memberExpression =>
                VisitMemberExpression(memberExpression, parameters),

            ConstantExpression constantExpression =>
                VisitConstantExpression(constantExpression.Value, parameters),

            NewExpression newExpression =>
                VisitConstantExpression(EvaluateExpression(newExpression), parameters),

            MemberInitExpression memberInitExpression =>
                VisitConstantExpression(EvaluateExpression(memberInitExpression), parameters),

            UnaryExpression unaryExpression
                when unaryExpression.NodeType == ExpressionType.Convert =>
                VisitExpression(unaryExpression.Operand, parameters),

            UnaryExpression unaryExpression
                when unaryExpression.NodeType == ExpressionType.Not =>
                $"(NOT {VisitExpression(unaryExpression.Operand, parameters)})",

            _ => throw new NotSupportedException(
                $"Unsupported expression type: {expressionNode.NodeType}")
        };
    }

    private static string VisitBinaryExpression(BinaryExpression binaryExpression, Dictionary<string, object> parameters)
    {
        if (binaryExpression.NodeType is ExpressionType.AndAlso /*&&*/ or ExpressionType.OrElse /*||*/)
        {
            var sqlOperator = binaryExpression.NodeType == ExpressionType.AndAlso
                ? "AND"
                : "OR";

            var leftSql = VisitExpression(binaryExpression.Left, parameters);
            var rightSql = VisitExpression(binaryExpression.Right, parameters);

            return $"({leftSql} {sqlOperator} {rightSql})";
        }

        if (IsNullConstant(binaryExpression.Right) &&
            binaryExpression.NodeType is ExpressionType.Equal or ExpressionType.NotEqual)
        {
            var leftSql = VisitExpression(binaryExpression.Left, parameters);

            return $"{leftSql} {(binaryExpression.NodeType == ExpressionType.Equal ? "IS" : "IS NOT")} NULL";
        }

        var leftSideSql = VisitExpression(binaryExpression.Left, parameters);
        var rightSideSql = VisitExpression(binaryExpression.Right, parameters);

        var comparisonOperator = 
            binaryExpression.NodeType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "<>",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            _ => throw new NotSupportedException(
                $"Unsupported binary operator: {binaryExpression.NodeType}")
        };

        return $"({leftSideSql} {comparisonOperator} {rightSideSql})";
    }

    private static bool IsNullConstant(Expression expression) =>
        expression is ConstantExpression constant && constant.Value is null;

    private static string VisitMemberExpression(MemberExpression memberExpression, Dictionary<string, object> parameters)
    {
        if (memberExpression.Expression is ParameterExpression) 
        {
            var colAttr = memberExpression.Member
                .GetCustomAttributes(typeof(DbColumnAttribute), inherit: true)
                .FirstOrDefault() as DbColumnAttribute;

            var colName = colAttr?.ColumnName ?? memberExpression.Member.Name;
            return $"[{colName}]";
        }

        var evaluatedValue = EvaluateExpression(memberExpression); //Program+<>c__DisplayClass0_0
        return VisitConstantExpression(evaluatedValue, parameters);
    }

    private static string VisitConstantExpression(object? value, IDictionary<string, object> parameters)
    {
        var paramName = $"@p{parameters.Count}";
        parameters[paramName] = value ?? DBNull.Value;
        return paramName;
    }

    private static object? EvaluateExpression(Expression expression)
    {
        var lambda = Expression.Lambda(expression);
        return lambda.Compile().DynamicInvoke();
    }
}
