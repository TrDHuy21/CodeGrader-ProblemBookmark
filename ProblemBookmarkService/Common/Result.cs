using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class Result
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        
        public List<ErrorDetail>? ErrorDetail {  get; set; }

        public Result()
        {
        }

        protected Result(string message, bool isSuccess, List<ErrorDetail>? errorDetail)
        {
            Message = message;
            IsSuccess = isSuccess;
            this.ErrorDetail = errorDetail;
        }

        public static Result Success(string message)
        {
            return new Result(message, true, null);
               
        }

        public static Result Failure(string message, List<ErrorDetail>? errorDetail)
        {
            return new Result(message, false, errorDetail);
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result()
        {
        }
        protected Result(string message, bool isSuccess, List<ErrorDetail>? errorDetail, T? data) : base(message, isSuccess, errorDetail)
        {
            Data = data;
        }

        public static Result<T> Success(string message, T? data)
        {
            return new Result<T>(message, true, null, data);
        }

        public static new Result<T> Failure(string message, List<ErrorDetail>? errorDetail)
        {
            return new Result<T>(message, false, errorDetail, default);
        }
    }
}
