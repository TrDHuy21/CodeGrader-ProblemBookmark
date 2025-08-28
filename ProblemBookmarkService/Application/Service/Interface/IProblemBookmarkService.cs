using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.ExternalDtos;
using Application.Dtos.ResponseDtos;
using Common;
using Microsoft.Identity.Client;

namespace Application.Service.Interface
{
    public interface IProblemBookmarkService
    {
        Task<Result> Remove(string problemId);
        Task<Result> Add(string problemId);
        Task<Result<List<IndexProblemExternalDto>>> GetProblemsByUser();
    }
}  
