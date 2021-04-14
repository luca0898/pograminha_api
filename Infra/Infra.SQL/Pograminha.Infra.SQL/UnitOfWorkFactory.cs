using Microsoft.EntityFrameworkCore;
using Pograminha.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pograminha.Infra.SQL
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<UnitOfWork>
    {
        private readonly DbContext _dbContext;
        public UnitOfWorkFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dbContext);
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new UnitOfWork(_dbContext, isolationLevel);
        }
    }
}
