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
    public partial interface IFieldRepository :IBaseRepository<Category>
    {
    }
    public partial class CategoryRepository :BaseRepository<Category>, IFieldRepository
    {
         public CategoryRepository(DbContext dbContext) : base(dbContext)
         {
         }

        public void AddRange(IEnumerable<Field> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Field> entities)
        {
            throw new NotImplementedException();
        }

        public void Create(Field entity)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Field entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Field entity)
        {
            throw new NotImplementedException();
        }

        public Field FirstOrDefault(Expression<Func<Field, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Field> FirstOrDefaultAsync(Expression<Func<Field, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Field> Get(Expression<Func<Field, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Field> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Field entity)
        {
            throw new NotImplementedException();
        }

        Field IBaseRepository<Field>.FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        Task<Field> IBaseRepository<Field>.FirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }

        Field IBaseRepository<Field>.Get<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }

        IQueryable<Field> IBaseRepository<Field>.Get()
        {
            throw new NotImplementedException();
        }

        Task<Field> IBaseRepository<Field>.GetAsync<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}

