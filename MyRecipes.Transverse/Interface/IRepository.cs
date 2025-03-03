namespace MyRecipes.Transverse.Interface
{
    /// <summary>
    /// Interface avec les command Crud de base 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// ajoute l'entité au context
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// ajoute toutes les entité au context
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
        /// <summary>
        /// retourne l'entité rechercher
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(TKey key);
        /// <summary>
        /// retourne toutes les entité
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<TEntity>> GetAllAsync();
        /// <summary>
        /// supprime l'entitée envoyer
        /// </summary>
        /// <param name="entitie"></param>
        /// <returns></returns>
        public Task RemoveAsync(TEntity entitie);
        /// <summary>
        /// supprime toutes les entitées envoyées
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task RemoveRangeAsync(ICollection<TEntity> entities);
        /// <summary>
        /// modifie l'entitie de façon asynchrone
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(TEntity entity);
        /// <summary>
        /// modifie les entities de façon asynchrone
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task UpdateRangeAsync(ICollection<TEntity> entities);
        /// <summary>
        /// Sauvegarde les changement dans le contexte de façon asynchrone
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync();
        /// <summary>
        /// retourne l'entité pour la function
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Func<TEntity, bool> predicate);
    }
}
