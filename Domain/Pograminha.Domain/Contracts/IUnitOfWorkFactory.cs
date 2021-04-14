using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pograminha.Domain.Contracts
{
    public interface IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        IUnitOfWork Create();
        IUnitOfWork Create(IsolationLevel isolationLevel);
    }
}
