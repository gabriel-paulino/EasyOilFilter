using Dapper;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Infra.Data.Session;

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
                        ([Id], [PurchaseId], [PaymentDate], [AmountPaid], [BankAccount], [Status]) 
                    VALUES 
                        (@Id, @PurchaseId, @PaymentDate, @AmountPaid, @BankAccount, @Status)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                payment.Id,
                payment.PurchaseId,
                payment.PaymentDate,
                payment.AmountPaid,
                payment.BankAccount,
                payment.Status
            });

            return rowsAffected == 1;
        }

        public async Task<IEnumerable<Payment>> GetPaidByPurchaseId(Guid purchaseId)
        {
            string query = @"
                SELECT
                    [Id],
                    [PurchaseId],                 
                    [PaymentDate],
                    [AmountPaid],
                    [BankAccount],
                    [Status]
                FROM 
                    [Payment]
                WHERE
                    [PurchaseId] = @PurchaseId AND
                    [Status] = @Status
            ";

            var payments = await _session.Connection.QueryAsync<Payment>(query, new
            {
                PurchaseId = purchaseId,
                Status = (int)PaymentStatus.Done
            },
            _session.Transaction
            );

            return payments.OrderByDescending(payment => payment.PaymentDate);
        }

        public async Task<IEnumerable<Payment>> GetAllByPurchaseId(Guid purchaseId)
        {
            string query = @"
                SELECT
                    [Id],
                    [PurchaseId],                 
                    [PaymentDate],
                    [AmountPaid],
                    [BankAccount],
                    [Status]
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

        public async Task<bool> Cancel(Guid id)
        {
            string command =
                @"
                    UPDATE [Payment] 
                    SET
                        [Status] = @Status
                    WHERE 
                        [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                Id = id,
                Status = (int)PaymentStatus.Canceled
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }
    }
}
