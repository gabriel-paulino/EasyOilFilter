using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class OilRepository : IOilRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public OilRepository(DbSession session)
        {
            _session = session;
        }

        ~OilRepository()
        {
            Dispose();
        }

        public async Task<bool> Create(Oil oil)
        {
            string command =
                @"
                    INSERT INTO [Oil]
                        ([Id], [Name], [Viscosity], [Price], [Type], [UnitOfMeasurement], [StockQuantity]) 
                    VALUES 
                        (@Id, @Name, @Viscosity, @Price, @Type, @UnitOfMeasurement, @StockQuantity)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.Price,
                oil.StockQuantity,
                Type = (int)oil.Type,
                UnitOfMeasurement = (int)oil.UnitOfMeasurement
            });

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Oil>> Get(int page, int quantity)
        {
            string query = (@"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                ORDER BY [Name]
                OFFSET @Page ROWS 
                FETCH NEXT @Quantity ROWS ONLY
            ");

            return await _session.Connection.QueryAsync<Oil>(query, new { Page = (page - 1) * quantity, Quantity = quantity });
        }

        public async Task<Oil> Get(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Oil>(query, new { Id = id });
        }

        public async Task<IEnumerable<Oil>> Get(OilType type)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Type] = @Type
            ";

            var oils =  await _session.Connection.QueryAsync<Oil>(query, new { Type = (int)type });
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> GetAll()
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query);
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> Get(string name = "", string viscosity = "", OilType type = OilType.All)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                /**where**/
            ";

            var builder = new SqlBuilder();

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"{name}%" });

            if (!string.IsNullOrEmpty(viscosity))
                builder.Where("[Viscosity] LIKE @Viscosity", new { Viscosity = $"{viscosity}%" });
            
            if(type != OilType.All)
                builder.Where("[Type] = @Type", new { Type = type });

            var templete = builder.AddTemplate(query);

            var oils = await _session.Connection.QueryAsync<Oil>(templete.RawSql, templete.Parameters);
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> GetByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Name] LIKE @Name
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new { Name = $"{name}%" });
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> GetByViscosity(string viscosity)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Viscosity] LIKE @Viscosity
            ";

            var oils =  await _session.Connection.QueryAsync<Oil>(query, new { Viscosity = $"{viscosity}%" });
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<bool> Update(Oil oil)
        {
            string command =
                @"
                    UPDATE [Oil] 
                    SET
                        [Name] = @Name,
                        [Viscosity] = @Viscosity,
                        [Price] = @Price,
                        [StockQuantity] = @StockQuantity,
                        [Type] = @Type,
                        [UnitOfMeasurement] = @UnitOfMeasurement
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.Price,
                oil.StockQuantity,
                Type = (int)oil.Type,
                UnitOfMeasurement = (int)oil.UnitOfMeasurement,
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> UpdatePrice(Guid id, decimal price)
        {
            string command =
                @"
                    UPDATE [Oil] 
                    SET
                        [Price] = @Price
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id,
                Price = price
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}