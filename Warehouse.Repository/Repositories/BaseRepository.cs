using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using Warehouse.Extension;
using Warehouse.Extension.Attributes;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbConnection _connection;
    private readonly string _entityName;
    private readonly string _tableName;

    protected BaseRepository(DbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _entityName = typeof(T).Name.Replace("Dto", string.Empty);

        if (typeof(T)
                .GetCustomAttributes(typeof(DbTableAttribute), inherit: false)
                .FirstOrDefault() is not DbTableAttribute dbTableAttribute)
            throw new InvalidOperationException($"The {typeof(T).Name} type must have a DbTableAttribute.");
        _tableName = dbTableAttribute.TableName;
    }

    public T? Get(object id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        return _connection.QueryFirstOrDefault<T>(
            $"udp_Get{_tableName}",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<T> Load(Expression<Func<T, bool>> expression)
    {
        var (whereQuery, parameters) = expression.Build();
        var sql = $"SELECT * FROM {_tableName} WHERE {whereQuery}";
        var dynamicParameters = new DynamicParameters(parameters);

        return _connection.Query<T>(sql, dynamicParameters);
    }

    public int Insert(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        var properties = typeof(T).GetProperties();
        var parameters = new DynamicParameters();

        AssignInsertParameters(entity, properties, parameters);
        _connection.Execute($"udp_Insert{_entityName}", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<int>($"@{_entityName}Id");
    }

    private void AssignInsertParameters(T entity, PropertyInfo[] properties, DynamicParameters parameters)
    {
        foreach (var property in properties)
        {
            if (Attribute.IsDefined(property, typeof(IgnoreForInsertAttribute)))
                continue;
            var value = property.GetValue(entity);
            parameters.Add($"@{property.Name}", value);
        }

        parameters.Add($"@{_entityName}Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
    }

    public void Update(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new DynamicParameters();

        AssignUpdateParameters(entity, properties, parameters);
        _connection.Execute($"udp_Update{_entityName}", parameters, commandType: CommandType.StoredProcedure);
    }

    private static void AssignUpdateParameters(T entity, PropertyInfo[] properties, DynamicParameters parameters)
    {
        foreach (var property in properties)
        {
            if (Attribute.IsDefined(property, typeof(IgnoreForUpdateAttribute)))
                continue;
            parameters.Add($"@{property.Name}", property.GetValue(entity));
        }
    }

    public void Delete(object id) =>
        _connection.Execute($"udp_Delete{_entityName}", new { id }, commandType: CommandType.StoredProcedure);
}