using System.Data;
using System.Data.SqlClient;

namespace EasyOilFilter.Infra.Data.Session
{
    public class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(SqlSettings settings)
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection(settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}