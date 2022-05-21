using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public FilterRepository(DbSession session)
        {
            _session = session;
        }

        ~FilterRepository()
        {
            Dispose();
        }

        public async Task<bool> Create(Filter filter)
        {
            string command =
                @"
                    INSERT INTO [Filter]
                        ([Id], [Code], [Manufacturer], [Price], [Type], [StockQuantity]) 
                    VALUES 
                        (@Id, @Code, @Manufacturer, @Price, @Type, @StockQuantity)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Code,
                filter.Manufacturer,
                filter.Price,
                filter.StockQuantity,
                Type = (int)filter.Type
            });

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Filter>> Get(int page, int quantity)
        {
            string query = (@"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                ORDER BY [Code]
                OFFSET @Page ROWS 
                FETCH NEXT @Quantity ROWS ONLY
            ");

            return await _session.Connection.QueryAsync<Filter>(query, new { Page = (page - 1) * quantity, Quantity = quantity });
        }

        public async Task<IEnumerable<Filter>> Get(string code = "", string manufacturer = "", FilterType type = FilterType.All)
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                /**where**/
            ";

            var builder = new SqlBuilder();

            if (!string.IsNullOrEmpty(code))
                builder.Where("[Name] LIKE @Code", new { Code = $"{code}%" });

            if (!string.IsNullOrEmpty(manufacturer))
                builder.Where("[Manufacturer] LIKE @Manufacturer", new { Manufacturer = $"{manufacturer}%" });

            if (type != FilterType.All)
                builder.Where("[Type] = @Type", new { Type = type });

            var templete = builder.AddTemplate(query);

            var filters = await _session.Connection.QueryAsync<Filter>(templete.RawSql, templete.Parameters);
            return filters.OrderByDescending(filter => filter.Code);
        }

        public async Task<Filter> Get(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Filter>(query, new { Id = id });
        }

        public async Task<IEnumerable<Filter>> Get(FilterType type)
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                WHERE
                    [Type] = @Type
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new { Type = (int)type });
            return filters.OrderByDescending(filter => filter.Code);
        }

        public async Task<IEnumerable<Filter>> GetAll()
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query);
            return filters.OrderByDescending(filter => filter.Code);
        }

        public async Task<IEnumerable<Filter>> GetByCode(string code)
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                WHERE
                    [Code] LIKE @Code
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new { Code = $"{code}%" });
            return filters.OrderByDescending(filter => filter.Code);
        }

        public async Task<IEnumerable<Filter>> GetByManufacturer(string manufacturer)
        {
            string query = @"
                SELECT
                    [Id],
                    [Code],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [Type]
                FROM 
                    [Filter]
                WHERE
                    [Manufacturer] LIKE @Manufacturer
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new { Manufacturer = $"{manufacturer}%" });
            return filters.OrderByDescending(filter => filter.Code);
        }

        public async Task<bool> Update(Filter filter)
        {
            string command =
                @"
                    UPDATE [Filter] 
                    SET
                        [Code] = @Code,
                        [Manufacturer] = @Manufacturer,
                        [Price] = @Price,
                        [StockQuantity] = @StockQuantity,
                        [Type] = @Type
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Code,
                filter.Manufacturer,
                filter.Price,
                filter.StockQuantity,
                Type = (int)filter.Type,
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> UpdatePrice(Guid id, decimal price)
        {
            string command =
                @"
                    UPDATE [Filter] 
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