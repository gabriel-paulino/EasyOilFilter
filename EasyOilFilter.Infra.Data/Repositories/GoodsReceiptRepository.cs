using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public GoodsReceiptRepository(DbSession session)
        {
            _session = session;
        }

        ~GoodsReceiptRepository()
        {
            Dispose();
        }

        public async Task<bool> AddHeader(GoodsReceipt goodsReceipt)
        {
            string command =
                @"
                    INSERT INTO [GoodsReceipt]
                        ([Id], [Provider], [Total], [Date], [Remarks], [Status]) 
                    VALUES 
                        (@Id, @Provider, @Total, @Date, @Remarks, @Status)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                goodsReceipt.Id,
                goodsReceipt.Provider,
                goodsReceipt.Total,
                goodsReceipt.Date,
                goodsReceipt.Remarks,
                Status = (int)goodsReceipt.Status
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> AddItem(GoodsReceiptItem item)
        {
            string command =
                @"
                    INSERT INTO [GoodsReceiptItem]
                        ([Id], [GoodsReceiptId], [ProductId], [ItemDescription], [UnitOfMeasurement], [Quantity], [UnitaryPrice], [TotalItem]) 
                    VALUES 
                        (@Id, @GoodsReceiptId, @ProductId, @ItemDescription, @UnitOfMeasurement, @Quantity, @UnitaryPrice, @TotalItem)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                item.Id,
                item.GoodsReceiptId,
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

        public async Task<IEnumerable<GoodsReceipt>> Get(DateTime date)
        {
            string query = @"
                SELECT
                    T0.[Id],
                    T0.[Provider],
                    T0.[Total],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    T1.[Id],
                    T1.[GoodsReceiptId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [GoodsReceipt] T0
                JOIN [GoodsReceiptItem] T1
                    ON T0.[Id] = T1.[GoodsReceiptId]
                WHERE 
                    T0.[Date] = @Date AND
                    T0.[Status] = @Status
            ";

            var goodsReceiptMap = new Dictionary<Guid, GoodsReceipt>();

            await _session.Connection.QueryAsync<GoodsReceipt, GoodsReceiptItem, GoodsReceipt>(query,
                map: (goodsReceipt, goodsReceiptItem) =>
                {
                    goodsReceiptItem.SetGoodsReceiptId(goodsReceipt.Id);

                    if (goodsReceiptMap.TryGetValue(goodsReceipt.Id, out GoodsReceipt? existingGoodsReceipt))
                        goodsReceipt = existingGoodsReceipt;
                    else
                        goodsReceiptMap.Add(goodsReceipt.Id, goodsReceipt);

                    goodsReceipt.AddItem(goodsReceiptItem);
                    return goodsReceipt;
                },
                param: new
                {
                    Date = date,
                    Status = (int)DocumentStatus.Finished
                }
            );

            return goodsReceiptMap.Values;
        }

        public async Task<GoodsReceipt?> Get(Guid id)
        {
            string query = @"
                SELECT 
                    T0.[Id],
                    T0.[Provider],
                    T0.[Total],
                    T0.[Date],
                    T0.[Remarks],
                    T0.[Status],
                    T1.[Id],
                    T1.[GoodsReceiptId],
                    T1.[ProductId],
                    T1.[ItemDescription],
                    T1.[UnitOfMeasurement],
                    T1.[Quantity],
                    T1.[UnitaryPrice],
                    T1.[TotalItem]
                FROM [GoodsReceipt] T0
                JOIN [GoodsReceiptItem] T1
                    ON T0.[Id] = T1.[GoodsReceiptId]
                WHERE 
                    T0.[Id] = @Id
            ";

            var result = await _session.Connection.QueryAsync<GoodsReceipt, GoodsReceiptItem, GoodsReceipt>(query,
                (goodsReceipt, goodsReceiptItem) =>
                {
                    goodsReceipt.AddItem(goodsReceiptItem);
                    return goodsReceipt;
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
                    UPDATE [GoodsReceipt] 
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
