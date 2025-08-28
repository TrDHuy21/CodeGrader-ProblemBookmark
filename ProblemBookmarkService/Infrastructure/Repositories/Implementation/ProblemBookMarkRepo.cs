using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementation
{
    public class ProblemBookMarkRepo : RenericRepo<ProblemBookmark>, IProblemBookmarkRepo
    {
        private ProblemBookmarkContext _context;

        public ProblemBookMarkRepo(ProblemBookmarkContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProblemBookmark problemBookmark)
        {
            await _context.Set<ProblemBookmark>().AddAsync(problemBookmark);
        }


        public async Task<Result<List<string>>> GetProblemIdsByUserId(string userId)
        {
            List<string> problemIds;
            try
            {
                problemIds = await _context.Set<ProblemBookmark>()
                .Where(p => p.UserId == userId)
                .Select(e => e.ProblemId)
                .ToListAsync();
            }catch(Exception ex)
            {
                return Result<List<string>>.Failure(ex.Message, null);
            }

            return Result<List<string>>.Success(string.Empty, problemIds);
        }

        public void Remove(ProblemBookmark problemBookmark)
        {
            _context.Set<ProblemBookmark>().Remove(problemBookmark);
        }
    }
}
