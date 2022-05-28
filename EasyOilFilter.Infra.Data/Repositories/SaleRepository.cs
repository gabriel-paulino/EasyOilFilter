using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public SaleRepository(DbSession session)
        {
            _session = session;
        }

        ~SaleRepository()
        {
            Dispose();
        }

        public async Task<bool> AddHeader(Sale sale)
        {
            string command =
                @"
                    INSERT INTO [Sale]
                        ([Id], [Description], [PaymentMethod], [Total], [Discount], [Date], [Remarks], [Status]) 
                    VALUES 
                        (@Id, @Description, @PaymentMethod, @Total, @Discount, @Date, @Remarks, @Status)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                sale.Id,
                sale.Description,
                PaymentMethod = (int)sale.PaymentMethod,
                sale.Total,
                sale.Discount,
                sale.Date,
                sale.Remarks,
                Status = (int)sale.Status
            }, 
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> AddItem(SaleItem item)
        {
            string command =
                @"
                    INSERT INTO [SaleItem]
                        ([Id], [SaleId], [ProductId], [ItemDescription], [UnitOfMeasurement], [Quantity], [UnitaryPrice], [TotalItem]) 
                    VALUES 
                        (@Id, @SaleId, @ProductId, @ItemDescription, @UnitOfMeasurement, @Quantity, @UnitaryPrice, @TotalItem)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                item.Id,
                item.SaleId,
                item.ProductId,
                item.ItemDescription,
                UnitOfMeasurement = (int)item.UnitOfMeasurement,
                item.Quantity,
                item.UnitaryPrice,
                item.TotalItem
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Sale>> Get(DateTime date)
        {
            string query = @"
                SELECT
                    T0.[Id],
                    T0.[Description],
                    T0.[PaymentMethod],
                    T0.[Total],
                    T0.[Discount],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    T1.[Id],
                    T1.[SaleId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [Sale] T0
                JOIN [SaleItem] T1
                    ON T0.[Id] = T1.[SaleId]
                WHERE 
                    T0.[Date] = @Date
            ";

            var saleMap = new Dictionary<Guid, Sale>();

            await _session.Connection.QueryAsync<Sale, SaleItem, Sale>(query,
                map: (sale, saleItem) =>
                {
                    saleItem.SaleId = sale.Id;

                    if (saleMap.TryGetValue(sale.Id, out Sale? existingSale))
                        sale = existingSale;
                    else
                        saleMap.Add(sale.Id, sale);

                    sale.AddItem(saleItem);
                    return sale;
                },
                //splitOn: "SaleItemId",
                param: new { date }
            );

            return saleMap.Values;
        }

        public async Task<Sale> Get(Guid id)
        {
            string query = @"
                SELECT 
                    T0.[Id],
                    T0.[Description],
                    T0.[PaymentMethod],
                    T0.[Total],
                    T0.[Discount],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    T1.[Id],
                    T1.[SaleId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [Sale] T0
                JOIN [SaleItem] T1
                    ON T0.[Id] = T1.[SaleId]
                WHERE 
                    T0.[Id] = @Id
            ";

            var result =  await _session.Connection.QueryAsync<Sale, SaleItem, Sale>(query,
                (sale, saleItem) =>
                {
                    sale.AddItem(saleItem);
                    return sale;
                },
             //splitOn: "SaleItemId",
             param: new { Id = id });

            return result?.FirstOrDefault() ?? new Sale();
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}