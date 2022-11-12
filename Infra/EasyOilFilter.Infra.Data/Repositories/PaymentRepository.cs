using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Infra.Data.Session;
using System.Xml.Linq;

namespace EasyOilFilter.Infra.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public PaymentRepository(DbSession session)
        {
            _session = session;
        }

        ~PaymentRepository()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task<bool> Add(Payment payment)
        {
            string command =
                @"
                    INSERT INTO [Payment]
                        ([Id], [PurchaseId], [PaymentDate], [AmountPaid]) 
                    VALUES 
                        (@Id, @PurchaseId, @PaymentDate, @AmountPaid)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                payment.Id,
                payment.PurchaseId,
                payment.PaymentDate,
                payment.AmountPaid
            });

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Payment>> Get(Guid purchaseId)
        {
            string query = @"
                SELECT
                    [Id],
                    [PurchaseId],                 
                    [PaymentDate],
                    [AmountPaid]
                FROM 
                    [Payment]
                WHERE
                    [PurchaseId] = @PurchaseId
            ";

            var payments = await _session.Connection.QueryAsync<Payment>(query, new
            {
                PurchaseId = purchaseId
            });

            return payments.OrderByDescending(payment => payment.PaymentDate);
        }
    }
}
