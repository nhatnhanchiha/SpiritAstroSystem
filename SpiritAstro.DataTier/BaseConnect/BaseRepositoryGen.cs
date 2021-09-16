using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SpiritAstro.DataTier.BaseConnect
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext;

        protected DbSet<TEntity> DbSet;
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }


        public TEntity FirstOrDefault()
        {
            return DbSet.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync()
        {
            return DbSet.FirstOrDefaultAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity Get<TKey>(TKey id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> Get()
        {
            return this.DbSet;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }
        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return await DbSet.FindAsync(id);
        }


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

    }
}

