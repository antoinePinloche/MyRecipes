namespace MyRecipes.Transverse.Interface
{
    /// <summary>
    /// Interface avec les command Crud de base 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {

        public Task<TEntity> AddAsync(TEntity entity);
        public Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
        public Task<TEntity> GetAsync(TKey key);
        public Task<ICollection<TEntity>> GetAllAsync();
        public Task RemoveAsync(TEntity entitie);
        public Task RemoveRangeAsync(ICollection<TEntity> entities);
        public Task UpdateAsync(TEntity entity);
        public Task UpdateRangeAsync(ICollection<TEntity> entities);
        public Task SaveAsync();
        public TEntity FirstOrDefault(Func<TEntity, bool> predicate);
    }
}
