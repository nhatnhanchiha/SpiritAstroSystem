using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpiritAstro.DataTier.BaseConnect
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected IUnitOfWork unitOfWork;
        protected IBaseRepository<TEntity> repository;
        public BaseService()
        {

        }
        
        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            repository.AddRange(entities);
            unitOfWork.Commit();
        }

        public async Task AddRangeAsyn(IEnumerable<TEntity> entities)
        {
            await repository.AddRangeAsync(entities);
            await SaveAsyn();
        }

        public int Count()
        {
            return repository.Count();
        }

        public void Create(TEntity entity)
        {
            repository.Create(entity);
            Save();
        }

        public async Task CreateAsyn(TEntity entity)
        {
            await repository.CreateAsync(entity);
            await SaveAsyn();
        }

        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
            Save();
        }

        public async Task DeleteAsyn(TEntity entity)
        {
            repository.Delete(entity);
            await SaveAsyn();
        }

        public TEntity FirstOrDefault()
        {
            return repository.FirstOrDefault();
        }

        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return repository.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsyn()
        {
            return await repository.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsyn(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return await repository.FirstOrDefaultAsync(predicate);
        }

        public TEntity Get<TKey>(TKey id)
        {
            return repository.Get(id);
        }

        public System.Linq.IQueryable<TEntity> Get()
        {
            return repository.Get();
        }

        public System.Linq.IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public async Task<TEntity> GetAsyn<TKey>(TKey id)
        {
            return await repository.GetAsync(id);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            repository.RemoveRange(entities);
            Save();
        }

        public async Task RemoveRangeAsyn(IEnumerable<TEntity> entities)
        {
            repository.RemoveRange(entities);
            await SaveAsyn();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public async Task SaveAsyn()
        {
            await unitOfWork.CommitAsync();
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
            Save();
        }

        public async Task UpdateAsyn(TEntity entity)
        {
            repository.Update(entity);
            await SaveAsyn();
        }
    }
}
