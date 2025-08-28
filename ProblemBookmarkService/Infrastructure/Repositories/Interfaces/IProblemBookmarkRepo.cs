using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProblemBookmarkRepo : IGenericRepo<ProblemBookmark>
    {
        Task<Result<List<string>>> GetProblemIdsByUserId(string userId);
        Task AddAsync(ProblemBookmark problemBookmark);

        void Remove(ProblemBookmark problemBookmark);


    }
}
