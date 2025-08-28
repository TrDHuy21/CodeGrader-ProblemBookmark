using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.ExternalDtos;
using Application.Dtos.ResponseDtos;
using Application.Service.Interface;
using Application.ServiceExternal.Interface;
using Common;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Service.Implementation
{
    public class ProblemBookmarkService : IProblemBookmarkService
    {
        protected IUnitOfWork _unitOfWork;
        protected IHttpContextAccessor _httpContext;
        protected IUserExternalService _userExternalService;
        protected IProblemExternalService _problemExternalService;

        public ProblemBookmarkService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext, IUserExternalService userExternalService, IProblemExternalService problemExternalService)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            _userExternalService = userExternalService;
            _problemExternalService = problemExternalService;
        }

        public async Task<Result> Add(string problemId)
        {
            var checkUserResult = await _userExternalService.ValidateCurrentUser();
            if (!checkUserResult.IsSuccess)
            {
                return checkUserResult;
            }
            var checkProblemResult = await _problemExternalService.ValidateProblem(problemId);
            if (!checkProblemResult.IsSuccess)
            {
                return checkProblemResult;
            }


            var problemBookmark = new ProblemBookmark()
            {
                UserId = _httpContext.HttpContext.User.FindFirst("Id")?.Value
                    ?? throw new Exception("Cannot get user id from token"),
                ProblemId = problemId
            };
            await _unitOfWork.ProblemBookmarks.AddAsync(problemBookmark);
            var result = await _unitOfWork.SaveChanges();

            if (result.IsSuccess)
            {
                return Result.Success("Bookmark problem success");
            }
            else
            {
                return result;
            }

        }

        public async Task<Result<List<IndexProblemExternalDto>>> GetProblemsByUser()
        {
            string userId = _httpContext.HttpContext.User.FindFirst("Id").Value;
            var problemIdsResult = await _unitOfWork.ProblemBookmarks.GetProblemIdsByUserId(userId);
            if (problemIdsResult.IsSuccess)
            {
                var indexProblemResult = await _problemExternalService.GetProblem(problemIdsResult.Data);
                return indexProblemResult;
            }
            else
            {
                return Result<List<IndexProblemExternalDto>>.Failure(problemIdsResult.Message, null);
            }
        }

        public async Task<Result> Remove(string problemId)
        {

            string userId = _httpContext.HttpContext.User.FindFirst("Id")?.Value
                    ?? throw new Exception("Cannot get user id from token");
            var problemBookmark = new ProblemBookmark()
            {
                UserId = userId,
                ProblemId = problemId
            };
            _unitOfWork.ProblemBookmarks.Remove(problemBookmark);
            var result = await _unitOfWork.SaveChanges();

            if (result.IsSuccess)
            {
                if (result.Data <= 0)
                {
                    return Result.Failure("This problem is not bookmarked", null);
                }
                return Result.Success("Delete bookmark problem success");
            }
            else
            {
                return result;
            }

        }
    }
}
