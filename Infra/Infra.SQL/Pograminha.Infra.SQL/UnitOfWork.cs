using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pograminha.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pograminha.Infra.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DbContext _dbContext;
        private bool _disposed = false;
        private bool _alreadyInTransaction = false;

        public UnitOfWork(DbContext dbContext, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (dbContext.Database.CurrentTransaction == null)
            {
                _transaction = dbContext.Database.BeginTransaction();
            }
            else
            {
                _alreadyInTransaction = true;
            }

            _dbContext = dbContext;
        }

        public virtual void Commit()
        {
            if (!_alreadyInTransaction)
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
            }
        }

        public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (!_alreadyInTransaction)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                _disposed = true;
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
