using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Entities.Reports;
using EasyOilFilter.Domain.Enums;
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
                        ([Id], [Description], [PaymentMethod], [Total], [Discount], [Date], [Time], [Remarks], [Status]) 
                    VALUES 
                        (@Id, @Description, @PaymentMethod, @Total, @Discount, @Date, @Time, @Remarks, @Status)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                sale.Id,
                sale.Description,
                PaymentMethod = (int)sale.PaymentMethod,
                sale.Total,
                sale.Discount,
                sale.Date,
                sale.Time,
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
                    T0.[Time],
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
                    T0.[Date] = @Date AND
                    T0.[Status] = @Status
            ";

            var saleMap = new Dictionary<Guid, Sale>();

            await _session.Connection.QueryAsync<Sale, SaleItem, Sale>(query,
                map: (sale, saleItem) =>
                {
                    saleItem.SetSaleId(sale.Id);

                    if (saleMap.TryGetValue(sale.Id, out Sale? existingSale))
                        sale = existingSale;
                    else
                        saleMap.Add(sale.Id, sale);

                    sale.AddItem(saleItem);
                    return sale;
                },
                param: new
                {
                    Date = date,
                    Status = (int)DocumentStatus.Finished
                }
            );

            return saleMap.Values;
        }

        public async Task<SaleReport> GetSaleReport(DateTime startDate, DateTime finalDate)
        {
            string query = @"
                SELECT
                    T0.[Date],
                    SUM(T0.[Total]) AS Total
                FROM [Sale] T0
                WHERE 
                    T0.[Date] >= @StartDate AND
                    T0.[Date] <= @FinalDate AND
                    T0.[Status] = @Status
                GROUP BY T0.[Date]

                SELECT
                    T0.[PaymentMethod],
                    SUM(T0.[Total]) AS Total
                FROM [Sale] T0
                WHERE 
                    T0.[Date] >= @StartDate AND
                    T0.[Date] <= @FinalDate AND
                    T0.[Status] = @Status
                GROUP BY T0.[PaymentMethod]
            ";

            var result = await _session.Connection.QueryMultipleAsync(query, new
            {
                StartDate = startDate,
                FinalDate = finalDate,
                Status = (int)DocumentStatus.Finished
            });

            return new SaleReport(
                salesByDate: result.Read<SaleByDate>().OrderBy(report => report.Date),
                salesByPaymentMethod: result.Read<SaleByPaymentMethod>()
                );
        }

        public async Task<Sale?> Get(Guid id)
        {
            string query = @"
                SELECT 
                    T0.[Id],
                    T0.[Description],
                    T0.[PaymentMethod],
                    T0.[Total],
                    T0.[Discount],
                    T0.[Date],
                    T0.[Time],
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

            var result = await _session.Connection.QueryAsync<Sale, SaleItem, Sale>(query,
                (sale, saleItem) =>
                {
                    sale.AddItem(saleItem);
                    return sale;
                },
             param: new
             {
                 Id = id
             });

            return result.FirstOrDefault();
        }

        public async Task<bool> Cancel(Guid id)
        {
            string command =
                @"
                    UPDATE [Sale] 
                    SET
                        [Status] = @Status
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id,
                Status = (int)DocumentStatus.Canceled
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<SoldItemReport>> GetSoldItems(DateTime startDate, DateTime finalDate)
        {
            string query = @"
                SELECT
                    T1.[ItemDescription] AS Name,
                    SUM(CASE
                            WHEN 
                                COALESCE(T2.[HasAlternative], 0) = 1 AND 
                                T1.[UnitOfMeasurement] != T2.[DefaultUoM] THEN T1.[Quantity] / 20
                            ELSE T1.[Quantity]
                        END) AS Quantity
                FROM [Sale] T0
                JOIN [SaleItem] T1
                    ON T0.[Id] = T1.[SaleId]
                LEFT JOIN [Product] T2
                    ON T2.[Id] = T1.[ProductId]
                WHERE 
                    T0.[Date] >= @StartDate AND
                    T0.[Date] <= @FinalDate AND
                    T0.[Status] = @Status
				GROUP BY T1.[ItemDescription]
            ";

            var items = await _session.Connection.QueryAsync<SoldItemReport>(query, new
            {
                StartDate = startDate,
                FinalDate = finalDate,
                Status = (int)DocumentStatus.Finished
            });

            return items.OrderByDescending(item => item.Name);
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}