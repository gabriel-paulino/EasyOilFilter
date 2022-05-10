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
                        ([Id], [Name], [Viscosity], [Price], [Type], [UnitOfMeasurement]) 
                    VALUES 
                        (@Id, @Name, @Viscosity, @Price, @Type, @UnitOfMeasurement)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.Price,
                Type = (int)oil.Type,
                UnitOfMeasurement = (int)oil.UnitOfMeasurement
            });

            return rowsAffected == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            string command =
                @"
                    DELETE [Oil]
                    WHERE 
                        [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new { Id = id }, _session.Transaction);

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
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                ORDER BY [Id]
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
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Type] = @Type
            ";

            return await _session.Connection.QueryAsync<Oil>(query, new { Type = (int)type });
        }

        public async Task<IEnumerable<Oil>> GetAll()
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
            ";

            return await _session.Connection.QueryAsync<Oil>(query);
        }

        public async Task<IEnumerable<Oil>> GetByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Name] = @Name
            ";

            return await _session.Connection.QueryAsync<Oil>(query, new { Name = name });
        }

        public async Task<IEnumerable<Oil>> GetByViscosity(string viscosity)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [Type],
                    [UnitOfMeasurement]
                FROM 
                    [Oil]
                WHERE
                    [Viscosity] = @Viscosity
            ";

            return await _session.Connection.QueryAsync<Oil>(query, new { Viscosity = viscosity });
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
                Platform = (int)oil.Type,
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
                        [Price] = @Price,
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