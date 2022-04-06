namespace RepositoryService
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetByYearAsync(string Year);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}