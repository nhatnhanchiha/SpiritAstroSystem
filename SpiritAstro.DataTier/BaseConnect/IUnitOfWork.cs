using System;
using System.Threading.Tasks;

namespace Test.DataTier.BaseConnect
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
