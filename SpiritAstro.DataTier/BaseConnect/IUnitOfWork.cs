using System;
using System.Threading.Tasks;

namespace SpiritAstro.DataTier.BaseConnect
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
