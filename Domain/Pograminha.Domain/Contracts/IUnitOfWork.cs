using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pograminha.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
