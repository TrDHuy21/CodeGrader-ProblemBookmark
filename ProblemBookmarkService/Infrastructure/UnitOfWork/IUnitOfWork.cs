using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Infrastructure.Repositories.Implementation;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ProblemBookMarkRepo ProblemBookmarks { get; }

        public Task<Result<int>> SaveChanges();

        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollBackAsync();


    }
}
