using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Infrastructure.Context;
using Infrastructure.Repositories.Implementation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProblemBookmarkContext _context;

        public UnitOfWork(ProblemBookmarkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ProblemBookMarkRepo? _problems;
        public ProblemBookMarkRepo ProblemBookmarks => _problems ??= new ProblemBookMarkRepo(_context);


        public async Task<Result<int>> SaveChanges()
        {
            try
            {
                var count = await _context.SaveChangesAsync();
                return Result<int>.Success("Success", count);

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        return Result<int>.Failure("Duplicate primary key", null);
                    default:
                        return Result<int>.Failure(ex.Message, null);
                }
            } 
            catch(DbUpdateConcurrencyException ex)
            {
                if(ex.Message.Contains("actually affected 0"))
                {
                    return Result<int>.Success("Success", 0);
                }
                return Result<int>.Failure(ex.Message, null);
            }

        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }


    }
}
