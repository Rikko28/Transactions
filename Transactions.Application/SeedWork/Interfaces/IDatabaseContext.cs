namespace Transactions.Application.SeedWork.Interfaces;
public interface IDatabaseContext
{
    bool HasActiveTransaction { get; }
    bool StartTransaction();
    void CommitTransaction();
    bool OpenConnection();
    void Rollback();

    Task<int> Execute(string sql, object? param = null);
    Task<T?> FirstOrDefault<T>(string sql, object? param = null);
}

