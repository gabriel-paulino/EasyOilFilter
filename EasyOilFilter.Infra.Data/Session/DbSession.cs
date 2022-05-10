using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyOilFilter.Infra.Data.Session
{
    public class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection(configuration.GetConnectionString("ConnectionString"));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}