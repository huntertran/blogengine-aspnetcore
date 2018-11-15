namespace BlogEngine.Repository.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> : IDisposable
    {
        IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void InsertRange(IList<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void DeleteRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);

        bool Exists(object id);

        void SaveChanges();

        Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetByIdAsync(object id);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(IList<TEntity> entities);

        Task DeleteAsync(object id);

        Task<bool> ExistsAsync(object id);

        Task SaveChangesAsync();
    }
}
