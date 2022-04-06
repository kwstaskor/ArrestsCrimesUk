using System.Data.Common;
using Dapper;

namespace RepositoryService;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbTransaction _transaction;
    private readonly string _tableName;

    public Repository(DbTransaction transaction ,string tableName)
    {
        _transaction = transaction;
        _tableName = tableName;
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        var sqlQuery = $"SELECT * FROM {_tableName}";
        await using (_transaction.Connection)
        {
            return await _transaction.Connection.QueryAsync<TEntity>(sqlQuery, transaction: _transaction);
        }
    }

    public async Task<IEnumerable<TEntity>> GetByYearAsync(string year)
    {
        var sqlQuery = $"SELECT * FROM {_tableName} WHERE [Time] LIKE '{year}%'";
        await using (_transaction.Connection)
        {
            return await _transaction.Connection.QueryAsync<TEntity>(sqlQuery,transaction:_transaction);
        }
    }

    public async Task<IEnumerable<TEntity>> GetByYearAndSexAsync( string year , string sex)
    {
        var sqlQuery = $"SELECT * FROM {_tableName} WHERE [Time] LIKE '{year}%' AND [GENDER] = '{sex}'";
        await using (_transaction.Connection)
        {
            return await _transaction.Connection.QueryAsync<TEntity>(sqlQuery, transaction: _transaction);
        }
    }
    
    public async Task<IEnumerable<TEntity>> GetByYearAndSexAndTownAsync( string year , string sex , string town)
    {
        var sqlQuery = $"SELECT * FROM {_tableName} WHERE [Time] LIKE '{year}%' AND [Gender] = '{sex}'AND [Geography] = '{town}'";
        await using (_transaction.Connection)
        {
            return await _transaction.Connection.QueryAsync<TEntity>(sqlQuery, transaction: _transaction);
        }
    }

    public void Create(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }
}