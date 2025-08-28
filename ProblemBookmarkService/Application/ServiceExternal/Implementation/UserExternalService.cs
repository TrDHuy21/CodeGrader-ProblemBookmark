using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.ExternalDtos;
using Application.ServiceExternal.Interface;
using Common;
using Domain.ExternalModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.ServiceExternal.Implementation
{
    public class UserExternalService : IUserExternalService
    {
        protected IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserExternalService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> ValidateCurrentUser()
        {
            try
            {
                var url = _configuration["ExternalService:UserService:GetByUsername"];
                var baseUrl = _configuration["ExternalService:BaseUrl"];
                

                if (string.IsNullOrEmpty(url))
                {
                    return Result.Failure("User service URL not configured", null);
                }

                // Lấy username từ token của user hiện tại
                var userNameClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Username");
                if (userNameClaim == null || string.IsNullOrEmpty(userNameClaim.Value))
                {
                    return Result.Failure("User not authenticated", null);
                }

                var client = _httpClientFactory.CreateClient("UserService");

                var requestUrl = $"{baseUrl}/{url}/{userNameClaim.Value}";
                Console.WriteLine(requestUrl);
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    var result = await response.Content.ReadFromJsonAsync<Result>();
                    if (result == null)
                    {
                        return Result.Failure("Invalid response from user service", null);
                    }

                    if (result.IsSuccess)
                    {
                        return Result.Success(string.Empty); 
                    }
                    else
                    {
                        return Result.Failure(result.Message ?? "User not found", result.ErrorDetail);
                    }
                }
                else
                {
                    return Result.Failure($"Cannot connect to user service: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure("Error validating user", null);
            }
        }
    }
}
