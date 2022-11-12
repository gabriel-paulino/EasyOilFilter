using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public PurchaseRepository(DbSession session)
        {
            _session = session;
        }

        ~PurchaseRepository()
        {
            Dispose();
        }

        public async Task<bool> AddHeader(Purchase purchase)
        {
            string command =
                @"
                    INSERT INTO [Purchase]
                        ([Id], [Provider], [Total], [Date], [Remarks], [Status]) 
                    VALUES 
                        (@Id, @Provider, @Total, @Date, @Remarks, @Status)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                purchase.Id,
                purchase.Provider,
                purchase.Total,
                purchase.Date,
                purchase.Remarks,
                Status = (int)purchase.Status
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> AddItem(PurchaseItem item)
        {
            string command =
                @"
                    INSERT INTO [PurchaseItem]
                        ([Id], [PurchaseId], [ProductId], [ItemDescription], [UnitOfMeasurement], [Quantity], [UnitaryPrice], [TotalItem]) 
                    VALUES 
                        (@Id, @PurchaseId, @ProductId, @ItemDescription, @UnitOfMeasurement, @Quantity, @UnitaryPrice, @TotalItem)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                item.Id,
                item.PurchaseId,
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

        public async Task<IEnumerable<Purchase>> Get(DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT
                    T0.[Id],
                    T0.[Provider],
                    T0.[Total],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    COALESCE(T2.[PaymentDone], 0) AS [PaymentDone],
                    T1.[Id],
                    T1.[PurchaseId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [Purchase] T0
                JOIN [PurchaseItem] T1
                    ON T0.[Id] = T1.[PurchaseId]
                LEFT JOIN (
					        SELECT 
						        [PurchaseId], 
						        SUM(AmountPaid) AS [PaymentDone]
					        FROM [Payment]
					        GROUP BY 
						        [PurchaseId]
					      ) T2 
					ON T0.[Id] = T2.[PurchaseId]
                WHERE 
                    T0.[Date] >= @StartDate AND
                    T0.[Date] <= @EndDate AND
                    T0.[Status] = @Status
                ORDER BY
                    T0.[Date]
            ";

            var purchaseMap = new Dictionary<Guid, Purchase>();

            await _session.Connection.QueryAsync<Purchase, PurchaseItem, Purchase>(query,
                map: (purchase, purchaseItem) =>
                {
                    purchaseItem.SetPurchaseId(purchase.Id);

                    if (purchaseMap.TryGetValue(purchase.Id, out Purchase? existingPurchase))
                        purchase = existingPurchase;
                    else
                        purchaseMap.Add(purchase.Id, purchase);

                    purchase.AddItem(purchaseItem);
                    return purchase;
                },
                param: new
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = (int)DocumentStatus.Finished
                }
            );

            return purchaseMap.Values;
        }

        public async Task<Purchase?> Get(Guid id)
        {
            string query = @"
                SELECT 
                    T0.[Id],
                    T0.[Provider],
                    T0.[Total],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    COALESCE(T2.[PaymentDone], 0) AS [PaymentDone],
                    T1.[Id],
                    T1.[PurchaseId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [Purchase] T0
                JOIN [PurchaseItem] T1
                    ON T0.[Id] = T1.[PurchaseId]
                LEFT JOIN (
					        SELECT 
						        [PurchaseId], 
						        SUM(AmountPaid) AS [PaymentDone]
					        FROM [Payment]
					        GROUP BY 
						        [PurchaseId]
					      ) T2 
					ON T0.[Id] = T2.[PurchaseId]
                WHERE 
                    T0.[Id] = @Id
            ";

            var result = await _session.Connection.QueryAsync<Purchase, PurchaseItem, Purchase>(query,
                (purchase, purchaseItem) =>
                {
                    purchase.AddItem(purchaseItem);
                    return purchase;
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
                    UPDATE [Purchase] 
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

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
