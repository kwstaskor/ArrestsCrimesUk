#nullable disable
using System.Data.Common;
using System.Data.SqlClient;

namespace RepositoryService;

public class UnitOfWork : IUnitOfWork
{
    private DbConnection _connection;
    private DbTransaction _transaction;
    private ArrestsRepos _arrests;
    private VictimsRepos _victims;
    private bool _dispose;
  
    public UnitOfWork(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public ArrestsRepos Arrests
    {
        get { return _arrests ?? (_arrests = new ArrestsRepos(_transaction)); }
    }
    public VictimsRepos Victims 
    {
        get { return _victims ?? (_victims = new VictimsRepos(_transaction)); }
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();  
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            resetRepositories();
        }
    }

    private void resetRepositories()
    {
        _arrests = null;
    }

    public void Dispose()
    {
        dispose(true);
        GC.SuppressFinalize(this);
    }

    private void dispose(bool disposing)
    {
        if (!_dispose)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
            _dispose = true;
        }
    }
    ~UnitOfWork()
    {
        dispose(false);
    }
}
