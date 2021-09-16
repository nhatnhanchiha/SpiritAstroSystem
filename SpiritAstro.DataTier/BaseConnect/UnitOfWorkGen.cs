using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpiritAstro.DataTier.BaseConnect
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public int Commit()
        {
            return this._dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return this._dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && this._dbContext != null)
            {
                this._dbContext.Dispose();
                this._dbContext = null;
            }
        }
    }
}
