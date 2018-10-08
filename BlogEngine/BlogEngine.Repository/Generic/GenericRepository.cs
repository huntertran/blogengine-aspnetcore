namespace BlogEngine.Repository.Generic
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // Expression<Func<TEntity, bool>> filter: Allow you pass in a LINQ filter function
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            // query is IQueryable, only executed when call ToList
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // next, it include properties by user
            var properties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var includeProperty in properties)
            {
                query = query.Include(includeProperty);
            }

            // finally, it translated to SQL and call DB
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        // Id can be GUID or int or string
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
