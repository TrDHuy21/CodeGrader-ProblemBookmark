using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Dtos.ExternalDtos; 
using Application.ServiceExternal.Interface;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.ServiceExternal.Implementation
{
    public class ProblemExternalService : IProblemExternalService
    {
        protected IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProblemExternalService(IConfiguration configuration, IHttpClientFactory httpClientFactory )
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result> ValidateProblem(string problemId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var baseUrl = _configuration["ExternalService:BaseUrl"];
                var url = _configuration["ExternalService:ProblemService:GetById"];
                var requestUrl = $"{baseUrl}/{url}/{problemId}";
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseResult = await response.Content.ReadFromJsonAsync<Result<IndexProblemExternalDto>>();

                    if (responseResult == null)
                    {
                        return Result<bool>.Failure("Invalid response from problem service", null);
                    }

                    if (responseResult.IsSuccess)
                    {
                        return Result.Success(string.Empty);
                    } 
                    else
                    {
                        return Result<bool>.Failure(responseResult.Message, null);
                    }
                }
                else
                {
                    return Result.Failure($"Cannot connect to problem service: {response.StatusCode}", null);

                }
            }
            catch
            {
                return Result.Failure("Error validating user", null);
            }
        }

        public async Task<Result<List<IndexProblemExternalDto>>> GetProblem(List<string> ids)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ExternalService:BaseUrl"];
            var url = _configuration["ExternalService:ProblemService:GetByListId"];
            var queryPrameter = "ids=" + string.Join("&ids=", ids);

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{url}?{queryPrameter}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                var problemListResult = await response.Content.ReadFromJsonAsync<Result<List<IndexProblemExternalDto>>>(
                        new JsonSerializerOptions()
                        {
                            NumberHandling = JsonNumberHandling.WriteAsString,
                            PropertyNameCaseInsensitive = true
                        }
                    );

                return problemListResult;
            }
            catch (Exception ex)
            {
                return Result<List<IndexProblemExternalDto>>.Failure(ex.Message, null);
            }
        }
    }
}
