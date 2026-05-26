using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using Warehouse.Extension;
using Warehouse.Extension.Attributes;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbConnection _connection;
    private readonly string _entityName;
    private readonly string _tableName;
    private readonly Func<DbTransaction?> _transactionProvider;

    protected BaseRepository(DbConnection connection, Func<DbTransaction?> transactionProvider)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _transactionProvider = transactionProvider ?? throw new ArgumentNullException(nameof(transactionProvider));
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
            new Dictionary<string, object>{{ $"{_entityName}Id", id }},
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<T> Load(Expression<Func<T, bool>> expression)
    {
        var (whereQuery, parameters) = expression.Build();
        var sql = $"SELECT * FROM {_tableName} WHERE {whereQuery}";
        var dynamicParameters = new DynamicParameters(parameters);

        return _connection.Query<T>(sql, dynamicParameters, transaction: _transactionProvider());
    }

    public virtual int Insert(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
       
        var properties = typeof(T).GetProperties();
        var parameters = new DynamicParameters();

        AssignParameters<IgnoreForInsertAttribute>(entity, properties, parameters);
        parameters.Add($"@{_entityName}Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        _connection.Execute(
            $"udp_Insert{_entityName}", 
            parameters, 
            transaction: _transactionProvider(), 
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>($"@{_entityName}Id");
    }

    public virtual void Update(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new DynamicParameters();

        AssignParameters<IgnoreForUpdateAttribute>(entity, properties, parameters);

        _connection.Execute(
            $"udp_Update{_entityName}", 
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);
    }

    public void Delete(object id) =>
        _connection.Execute(
            $"udp_Delete{_entityName}",
            new Dictionary<string, object> { { $"{_entityName}Id", id } },
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);

    protected static void AssignParameters<TAttribute>(T entity, PropertyInfo[] properties, DynamicParameters parameters) where TAttribute : Attribute
    {
        foreach (var property in properties)
        {
            if (Attribute.IsDefined(property, typeof(TAttribute)))
                continue;
            parameters.Add($"@{property.Name}", property.GetValue(entity));
        }
    }
}