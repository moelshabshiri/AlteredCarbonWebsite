using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefMessageSC(statusCode);
        }



        public int StatusCode { get; set; }
        public string Message { get; set; }



        private string GetDefMessageSC(int statusCode)
        {
            return statusCode switch
            {
            422 => "Unprocessable Entity",
            400 => "A bad request, you made",
            401 => "Unauthorized",
            404 => "Resource not found",
            500 => "Internal Server Error",
            _ => null};
        }
    }
}


