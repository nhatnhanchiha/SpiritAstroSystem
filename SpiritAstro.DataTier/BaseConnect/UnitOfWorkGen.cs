using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.DataTier.BaseConnect;

namespace SpiritAstro.DataTier.BaseConnect
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return this.dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && this.dbContext != null)
            {
                this.dbContext.Dispose();
                this.dbContext = null;
            }
        }
    }
}
