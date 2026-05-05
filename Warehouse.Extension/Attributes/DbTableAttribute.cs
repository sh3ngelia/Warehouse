namespace Warehouse.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DbTableAttribute : Attribute
    {
        public string TableName { get; }
        public DbTableAttribute(string tableName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tableName, nameof(tableName));

            TableName = tableName;
        }
    }
}
