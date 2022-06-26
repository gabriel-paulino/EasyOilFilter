using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
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
                        ([Id], [Name], [Manufacturer], [DefaultPrice], [FilterType], [Type], [DefaultUoM], [StockQuantity]) 
                    VALUES 
                        (@Id, @Name, @Manufacturer, @DefaultPrice, @FilterType, @Type, @DefaultUoM, @StockQuantity)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Name,
                filter.Manufacturer,
                filter.DefaultPrice,
                filter.StockQuantity,
                FilterType = (int)filter.FilterType,
                Type = (int)filter.Type,
                DefaultUoM = (int)filter.DefaultUoM
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
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [Type],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [Viscosity],
                    [OilType],
                    [Manufacturer],
                    [FilterType],
                    [HasAlternative]
                FROM 
                    [Product]
            ";

            var filters = await _session.Connection.QueryAsync<Product>(query);

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<Product> Get(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],                 
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [Type],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [Viscosity],
                    [OilType],
                    [Manufacturer],
                    [FilterType],
                    [HasAlternative]
                FROM 
                    [Product]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Product>(query, new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],                 
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [Type],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [Viscosity],
                    [Api],
                    [OilType],
                    [Manufacturer],
                    [FilterType],
                    [HasAlternative]
                FROM 
                    [Product]
                WHERE
                    [Name] LIKE @Name
            ";

            var products = await _session.Connection.QueryAsync<Product>(query, new
            {
                Name = $"%{name}%"
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
                    [DefaultPrice],
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

        public async Task<IEnumerable<Filter>> Get(string name = "", string manufacturer = "", FilterType type = FilterType.None)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Manufacturer],
                    [DefaultPrice],
                    [StockQuantity],
                    [FilterType]
                FROM 
                    [Product]
                /**where**/
            ";

            var builder = new SqlBuilder();

            builder.Where("[Type] = @Type", new { Type = (int)ProductType.Filter });

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"%{name}%" });

            if (!string.IsNullOrEmpty(manufacturer))
                builder.Where("[Manufacturer] LIKE @Manufacturer", new { Manufacturer = $"%{manufacturer}%" });

            if (type != FilterType.None)
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
                    [DefaultPrice],
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
                    [DefaultPrice],
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
                    [DefaultPrice],
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
                    [DefaultPrice],
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
                Name = $"%{name}%"
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
                    [DefaultPrice],
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
                Manufacturer = $"%{manufacturer}%"
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
                        [DefaultPrice] = @DefaultPrice,
                        [StockQuantity] = @StockQuantity,
                        [FilterType] = @FilterType
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                filter.Id,
                filter.Name,
                filter.Manufacturer,
                filter.DefaultPrice,
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
                        ([Id], [Name], [Viscosity], [Api], [DefaultPrice], [AlternativePrice], [OilType], [Type], [DefaultUoM], [AlternativeUoM], [StockQuantity], [HasAlternative]) 
                    VALUES 
                        (@Id, @Name, @Viscosity, @Api, @DefaultPrice, @AlternativePrice, @OilType, @Type, @DefaultUoM, @AlternativeUoM, @StockQuantity, @HasAlternative)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.Api,
                oil.DefaultPrice,
                oil.AlternativePrice,
                oil.StockQuantity,
                OilType = (int)oil.OilType,
                Type = (int)oil.Type,
                DefaultUoM = (int)oil.DefaultUoM,
                AlternativeUoM = (int)oil.AlternativeUoM,
                HasAlternative = oil.HasAlternative
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],                   
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
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

        public async Task<IEnumerable<Oil>> Get(string name = "", string viscosity = "", OilType type = OilType.None)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Viscosity],
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
                FROM 
                    [Product]
                /**where**/
            ";

            var builder = new SqlBuilder();

            builder.Where("[Type] = @Type", new { Type = (int)ProductType.Oil });

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"%{name}%" });

            if (!string.IsNullOrEmpty(viscosity))
                builder.Where("[Viscosity] LIKE @Viscosity", new { Viscosity = $"%{viscosity}%" });

            if (type != OilType.None)
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Name] LIKE @Name
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil,
                Name = $"%{name}%"
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
                    [Api],
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [OilType],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [HasAlternative]
                FROM 
                    [Product]
                WHERE
                    [Type] = @Type AND
                    [Viscosity] LIKE @Viscosity
            ";

            var oils = await _session.Connection.QueryAsync<Oil>(query, new
            {
                Type = (int)ProductType.Oil,
                Viscosity = $"%{viscosity}%"
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
                        [DefaultPrice] = @DefaultPrice,
                        [AlternativePrice] = @AlternativePrice,
                        [StockQuantity] = @StockQuantity,
                        [OilType] = @OilType,
                        [DefaultUoM] = @DefaultUoM,
                        [AlternativeUoM] = @AlternativeUoM,
                        [HasAlternative] = @HasAlternative
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                oil.Id,
                oil.Name,
                oil.Viscosity,
                oil.DefaultPrice,
                oil.AlternativePrice,
                oil.StockQuantity,
                OilType = (int)oil.OilType,
                DefaultUoM = (int)oil.DefaultUoM,
                AlternativeUoM = (int)oil.AlternativeUoM,
                HasAlternative = oil.HasAlternative,
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> UpdateDefaultPrice(Guid id, decimal defaultPrice)
        {
            string command =
                @"
                    UPDATE [Product] 
                    SET
                        [DefaultPrice] = @DefaultPrice
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id,
                DefaultPrice = defaultPrice
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
                    [DefaultPrice],
                    [AlternativePrice],
                    [StockQuantity],
                    [Type],
                    [DefaultUoM],
                    [AlternativeUoM],
                    [Viscosity],
                    [Api],
                    [OilType],
                    [Manufacturer],
                    [FilterType],
                    [HasAlternative]
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

        public async Task<bool> Delete(Guid id)
        {
            string command =
                @"
                    DELETE FROM [Product] 
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id
            });

            return rowsAffected == 1;
        }
    }
}