namespace Warehouse.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DbColumnAttribute : Attribute
    {
        public string ColumnName { get; }
        public DbColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
