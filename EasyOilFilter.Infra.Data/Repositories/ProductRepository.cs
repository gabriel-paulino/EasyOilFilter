using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public ProductRepository(DbSession session)
        {
            _session = session;
        }

        ~ProductRepository()
        {
            Dispose();
        }

        public async Task<bool> Create(Filter filter)
        {
            string command =
                @"
                    INSERT INTO [Product]
                        ([Id], [Name], [Manufacturer], [Price], [FilterType], [Type], [UnitOfMeasurement], [StockQuantity]) 
                    VALUES 
                        (@Id, @Name, @Manufacturer, @Price, @FilterType, @Type, @UnitOfMeasurement, @StockQuantity)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Name,
                filter.Manufacturer,
                filter.Price,
                filter.StockQuantity,
                FilterType = (int)filter.FilterType,
                Type = (int)filter.Type,
                UnitOfMeasurement = (int)filter.UnitOfMeasurement
            });

            return rowsAffected == 1;
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],                 
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement],
                    [Viscosity],
                    [OilType],
                    [Manufacturer],
                    [FilterType]                   
                FROM 
                    [Product]
            ";

            var filters = await _session.Connection.QueryAsync<Product>(query);

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],                 
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement],
                    [Viscosity],
                    [OilType],
                    [Manufacturer],
                    [FilterType]                   
                FROM 
                    [Product]
                WHERE
                    [Name] LIKE @Name
            ";

            var products = await _session.Connection.QueryAsync<Product>(query, new
            {
                Name = $"{name}%"
            });

            return products.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Filter>> GetFilters(int page, int quantity)
        {
            string query = (@"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type
                ORDER BY [Name]
                OFFSET @Page ROWS 
                FETCH NEXT @Quantity ROWS ONLY
            ");

            return await _session.Connection.QueryAsync<Filter>(query, new
            {
                Page = (page - 1) * quantity,
                Quantity = quantity,
                Type = (int)ProductType.Filter
            });
        }

        public async Task<IEnumerable<Filter>> Get(string name = "", string manufacturer = "", FilterType type = FilterType.All)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                /**where**/
            ";

            var builder = new SqlBuilder();

            builder.Where("[Type] = @Type", new { Type = (int)ProductType.Filter });

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"{name}%" });

            if (!string.IsNullOrEmpty(manufacturer))
                builder.Where("[Manufacturer] LIKE @Manufacturer", new { Manufacturer = $"{manufacturer}%" });

            if (type != FilterType.All)
                builder.Where("[FilterType] = @FilterType", new { FilterType = type });

            var templete = builder.AddTemplate(query);

            var filters = await _session.Connection.QueryAsync<Filter>(templete.RawSql, templete.Parameters);
            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<Filter> GetFilter(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Filter>(query, new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Filter>> Get(FilterType type)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [FilterType] = @FilterType
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new
            {
                Type = (int)ProductType.Filter,
                FilterType = (int)type
            });

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Filter>> GetAllFilters()
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new
            {
                Type = (int)ProductType.Filter
            });

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Filter>> GetFiltersByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Name] LIKE @Name
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new
            {
                Type = (int)ProductType.Filter,
                Name = $"{name}%"
            });

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Filter>> GetByManufacturer(string manufacturer)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [Price],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Manufacturer] LIKE @Manufacturer
            ";

            var filters = await _session.Connection.QueryAsync<Filter>(query, new
            {
                Type = (int)ProductType.Filter,
                Manufacturer = $"{manufacturer}%"
            });

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<bool> Update(Filter filter)
        {
            string command =
                @"
                    UPDATE [Product] 
                    SET
                        [Name] = @Name,
                        [Manufacturer] = @Manufacturer,
                        [Price] = @Price,
                        [StockQuantity] = @StockQuantity,
                        [FilterType] = @FilterType
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Name,
                filter.Manufacturer,
                filter.Price,
                filter.StockQuantity,
                FilterType = (int)filter.FilterType,
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> Create(Oil oil)
        {
            string command =
                @"
                    INSERT INTO [Product]
                        ([Id], [Name], [Viscosity], [Price], [OilType], [Type], [UnitOfMeasurement], [StockQuantity]) 
                    VALUES 
                        (@Id, @Name, @Viscosity, @Price, @OilType, @Type, @UnitOfMeasurement, @StockQuantity)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.Price,
                oil.StockQuantity,
                OilType = (int)oil.OilType,
                Type = (int)oil.Type,
                UnitOfMeasurement = (int)oil.UnitOfMeasurement
            });

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Oil>> GetOils(int page, int quantity)
        {
            string query = (@"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type
                ORDER BY [Name]
                OFFSET @Page ROWS 
                FETCH NEXT @Quantity ROWS ONLY
            ");

            return await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil,
                Page = (page - 1) * quantity,
                Quantity = quantity
            });
        }

        public async Task<Oil> GetOil(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Oil>(query, new
            {
                Id = id
            });
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
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [OilType] = @OilType        
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)type,
                OilType = (int)ProductType.Oil
            });

            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> GetAllOils()
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil
            });

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
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                /**where**/
            ";

            var builder = new SqlBuilder();

            builder.Where("[Type] = @Type", new { Type = (int)ProductType.Oil });

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"{name}%" });

            if (!string.IsNullOrEmpty(viscosity))
                builder.Where("[Viscosity] LIKE @Viscosity", new { Viscosity = $"{viscosity}%" });

            if (type != OilType.All)
                builder.Where("[OilType] = @OilType", new { OilType = type });

            var templete = builder.AddTemplate(query);

            var oils = await _session.Connection.QueryAsync<Oil>(templete.RawSql, templete.Parameters);
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<IEnumerable<Oil>> GetOilsByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Price],
                    [StockQuantity],
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Name] LIKE @Name
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil,
                Name = $"{name}%"
            }
            );
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
                    [OilType],
                    [UnitOfMeasurement]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Viscosity] LIKE @Viscosity
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil,
                Viscosity = $"{viscosity}%"
            }
            );
            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<bool> Update(Oil oil)
        {
            string command =
                @"
                    UPDATE [Product] 
                    SET
                        [Name] = @Name,
                        [Viscosity] = @Viscosity,
                        [Price] = @Price,
                        [StockQuantity] = @StockQuantity,
                        [OilType] = @OilType,
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
                OilType = (int)oil.OilType,
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
                    UPDATE [Product] 
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

        public async Task<IEnumerable<Product>> Get(IEnumerable<Guid> ids)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],                 
                    [Price],
                    [StockQuantity],
                    [Type],
                    [UnitOfMeasurement],
                    [Viscosity],
                    [OilType],
                    [Manufacturer],
                    [FilterType]                   
                FROM 
                    [Product]
                WHERE
                    [Id] IN @Ids
            ";

            var products = await _session.Connection.QueryAsync<Product>(query, new
            {
                Ids = ids
            },
            _session.Transaction
            );

            return products.OrderByDescending(filter => filter.Name);
        }

        public async Task<bool> SetStockQuantity(Guid id, decimal stockQuantity)
        {
            string command =
                @"
                    UPDATE [Product] 
                    SET
                        [StockQuantity] = @StockQuantity
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id,
                StockQuantity = stockQuantity
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }
    }
}