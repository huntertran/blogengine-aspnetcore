namespace BlogEngine.Repository.Generic
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            // query is IQueryable, only executed when call ToList
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        // Expression<Func<TEntity, bool>> filter: Allow you pass in a LINQ filter function
        public virtual IList<TEntity> Get(
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

            return query.ToList();
        }

        public virtual IList<TEntity> GetByPage(
            int page = 1,
            int itemPerPage = 5,
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

            // order
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // paging
            query = query.Skip((page - 1) * itemPerPage).Take(itemPerPage);

            // next, it include properties by user
            var properties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var includeProperty in properties)
            {
                query = query.Include(includeProperty);
            }

            // finally, it translated to SQL and call DB
            return query.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAsync(
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
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetByPageAsync(
            int page = 1,
            int itemPerPage = 5,
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

            // order
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // paging
            query = query.Skip((page - 1) * itemPerPage).Take(itemPerPage);

            // next, it include properties by user
            var properties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var includeProperty in properties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        // Id can be GUID or int or string
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void InsertRange(IList<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task InsertRangeAsync(IList<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await GetByIdAsync(id);

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

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            var listEntity = entities.ToList();
            foreach (var entityToDelete in listEntity)
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
            }

            _dbSet.RemoveRange(listEntity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public bool Exists(object id)
        {
            var item = GetById(id);
            return item != null;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            var item = await GetByIdAsync(id);
            return item != null;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
