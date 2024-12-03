﻿namespace Okala.CryptoExchange.WebApi.Middlewares
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Error = false,
                ErrorMessage = null
            };
        }

        public static ApiResponse<T> ErrorResponse(string errorMessage)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Error = true,
                ErrorMessage = errorMessage
            };
        }
    }
}
