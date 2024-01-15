using Dapper;
using System.Data;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Infrastructure;

public class SqlLiteDatabaseContext : IDatabaseContext, IDisposable
{
    private readonly IDbConnection _connection;
    private bool _disposed;
    private IDbTransaction? _transaction;

    public SqlLiteDatabaseContext(IDbConnection connection)
    {
        _connection = connection;
    }

    public bool HasActiveTransaction => _transaction != null;

    public bool StartTransaction()
    {
        if (!OpenConnection())
            return false;
        _transaction = _connection.BeginTransaction();
        return true;
    }

    public void CommitTransaction()
    {
        if (_transaction == null) return;
        try
        {
            _transaction.Commit();
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _connection.Close();
            _transaction = null;
        }
    }

    public bool OpenConnection()
    {
        if (_connection.State == ConnectionState.Open)
            return false;
        _connection.Open();
        return true;
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            _connection.Close();
        }
    }

    public async Task<T?> FirstOrDefault<T>(string sql, object? param = null)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction);
    }

    public async Task<int> Execute(string sql, object? param = null)
    {
        var result = await _connection.ExecuteAsync(sql, param, _transaction);
        return result;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            if (_connection.State == ConnectionState.Open) _transaction?.Rollback();
            _connection.Dispose();
            _transaction?.Dispose();
            _disposed = true;
        }
    }
}
