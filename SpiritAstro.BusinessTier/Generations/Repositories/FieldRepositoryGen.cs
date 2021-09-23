/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Repositories
{
    public partial interface IFieldRepository :IBaseRepository<Field>
    {
    }
    public partial class FieldRepository :BaseRepository<Field>, IFieldRepository
    {
         public FieldRepository(DbContext dbContext) : base(dbContext)
         {
         }

        public void AddRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public void Create(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category FirstOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Category> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        Category IBaseRepository<Category>.FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        Task<Category> IBaseRepository<Category>.FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        Category IBaseRepository<Category>.Get<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }

        IQueryable<Category> IBaseRepository<Category>.Get()
        {
            throw new NotImplementedException();
        }

        Task<Category> IBaseRepository<Category>.GetAsync<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}

