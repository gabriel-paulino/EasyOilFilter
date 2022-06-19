namespace EasyOilFilter.Infra.Data.Session
{
    public class SqlSettings
    {
        public SqlSettings(string connectionString) =>
            ConnectionString = connectionString;

        public string ConnectionString { get; }
    }
}