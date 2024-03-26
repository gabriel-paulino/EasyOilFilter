namespace EasyOilFilter.Domain.Contracts.UoW;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}
