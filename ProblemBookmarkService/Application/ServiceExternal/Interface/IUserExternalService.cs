using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.ExternalDtos;
using Common;

namespace Application.ServiceExternal.Interface
{
    public interface IUserExternalService
    {
        Task<Result> ValidateCurrentUser();
    }
}
