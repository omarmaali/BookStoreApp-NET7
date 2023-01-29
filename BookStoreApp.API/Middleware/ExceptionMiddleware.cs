using BookStoreApp.API.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Net;

namespace BookStoreApp.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        { 
        try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                // Log Error
                _logger.LogError(ex, $"Error while processing request {context.Request.Path}");
                
                // Handle error and return proper response
                await HandleExcetionAsync(context, ex);
            }
        }

        private Task HandleExcetionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                ErrorMessage = ex.Message,
                ErrorType = "Failure"
            };

            switch(ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case DbUpdateConcurrencyException:
                    statusCode = HttpStatusCode.Conflict;
                    errorDetails.ErrorType = "Db Update Concurrency Exception";
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }

        public class ErrorDetails
        {
            public string ErrorType { get; set; }
            public string ErrorMessage  { get; set; }
        }
    }
}
