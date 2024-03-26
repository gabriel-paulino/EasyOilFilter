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

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task<bool> CreateAsync<T>(T product) where T : Product
        {
            string command =
                @"
                    INSERT INTO [Product]
                        (
                            [Id], 
                            [Name], 
                            [Viscosity], 
                            [Api], 
                            [Manufacturer], 
                            [DefaultPrice], 
                            [AlternativePrice], 
                            [Type], 
                            [OilType], 
                            [FilterType], 
                            [DefaultUoM],
                            [AlternativeUoM], 
                            [StockQuantity], 
                            [HasAlternative]
                        ) 
                    VALUES 
                        (
                            @Id, 
                            @Name, 
                            @Viscosity, 
                            @Api, 
                            @Manufacturer, 
                            @DefaultPrice, 
                            @AlternativePrice,
                            @Type, 
                            @OilType, 
                            @FilterType, 
                            @DefaultUoM, 
                            @AlternativeUoM, 
                            @StockQuantity, 
                            @HasAlternative
                        )
                ";

            var oil = product as Oil;
            var filter = product as Filter;

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                product.Id,
                product.Name,
                oil?.Viscosity,
                oil?.Api,
                filter?.Manufacturer,
                product.DefaultPrice,
                product.AlternativePrice,
                Type = (int)product.Type,
                OilType = (int?)oil?.OilType,
                FilterType = (int?)filter?.FilterType,
                DefaultUoM = (int)product.DefaultUoM,
                AlternativeUoM = (int?)oil?.AlternativeUoM,
                product.StockQuantity,
                product.HasAlternative
            }).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        public async Task<bool> UpdateAsync<T>(T product) where T : Product
        {
            string command =
                @"
                    UPDATE [Product] 
                    SET
                        [Name] = @Name,
                        [Viscosity] = @Viscosity,
                        [Api] = @Api,
                        [Manufacturer] = @Manufacturer,
                        [DefaultPrice] = @DefaultPrice,
                        [AlternativePrice] = @AlternativePrice,
                        [StockQuantity] = @StockQuantity,
                        [OilType] = @OilType,
                        [FilterType] = @FilterType,
                        [DefaultUoM] = @DefaultUoM,
                        [AlternativeUoM] = @AlternativeUoM,
                        [HasAlternative] = @HasAlternative                      
                    WHERE [Id] = @Id
                ";

            var oil = product as Oil;
            var filter = product as Filter;

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                product.Id,
                product.Name,
                oil?.Viscosity,
                oil?.Api,
                filter?.Manufacturer,
                product.DefaultPrice,
                oil?.AlternativePrice,
                product.StockQuantity,
                OilType = (int?)oil?.OilType,
                FilterType = (int?)filter?.FilterType,
                DefaultUoM = (int?)product?.DefaultUoM,
                AlternativeUoM = (int?)oil?.AlternativeUoM,
                product?.HasAlternative,
            },
            _session.Transaction
            ).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : Product
        {
            var type = typeof(T);
            string query = GetQueryProduct<T>(type);
            var builder = new SqlBuilder();
            AddProductType<T>(builder, type);
            var templete = builder.AddTemplate(query);

            var products = await _session.Connection
                .QueryAsync<T>(templete.RawSql, templete.Parameters)
                .ConfigureAwait(false);

            return products.OrderByDescending(product => product.Name);
        }

        public async Task<Product?> GetAsync(Guid id)
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
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetByName<T>(string name) where T : Product
        {
            var type = typeof(T);
            string query = GetQueryProduct<T>(type);

            var builder = new SqlBuilder();

            if (!string.IsNullOrEmpty(name))
                builder.Where("[Name] LIKE @Name", new { Name = $"%{name}%" });

            AddProductType<T>(builder, type);

            var templete = builder.AddTemplate(query);

            var products = await _session.Connection
                .QueryAsync<T>(templete.RawSql, templete.Parameters)
                .ConfigureAwait(false);

            return products.OrderByDescending(product => product.Name);
        }

        public async Task<IEnumerable<Filter>> GetAsync(string name = "", string manufacturer = "", FilterType type = FilterType.None)
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

            var filters = await _session.Connection
                .QueryAsync<Filter>(templete.RawSql, templete.Parameters)
                .ConfigureAwait(false);

            return filters.OrderByDescending(filter => filter.Name);
        }

        public async Task<IEnumerable<Oil>> GetAsync(string name = "", string viscosity = "", OilType type = OilType.None)
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

            var oils = await _session.Connection
                .QueryAsync<Oil>(templete.RawSql, templete.Parameters)
                .ConfigureAwait(false);

            return oils.OrderByDescending(oil => oil.Name);
        }

        public async Task<bool> UpdateDefaultPriceAsync(Guid id, decimal defaultPrice)
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
            ).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
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
            ).ConfigureAwait(false);

            return products.OrderByDescending(filter => filter.Name);
        }

        public async Task<bool> SetStockQuantityAsync(Guid id, decimal stockQuantity)
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
            ).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            string command =
                @"
                    DELETE FROM [Product] 
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id
            }).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        private string GetQueryProduct<T>(Type type) where T : Product
        {
            if (type == typeof(Filter)) return
            @"
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

            if (type == typeof(Oil)) return
            @"
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

            return
            @"
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
                /**where**/
            ";
        }

        private void AddProductType<T>(SqlBuilder builder, Type type) where T : Product
        {
            if (type == typeof(Filter))
                builder.Where("[Type] = @Type", new { Type = (int)ProductType.Filter });

            if (type == typeof(Oil))
                builder.Where("[Type] = @Type", new { Type = (int)ProductType.Oil });
        }
    }
}