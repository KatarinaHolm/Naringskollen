using Microsoft.AspNetCore.Mvc;

namespace Naringskollen.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var (statusCode, title) = ex switch
                {
                    KeyNotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                    ArgumentException or InvalidOperationException => (StatusCodes.Status400BadRequest, "Bad Request"),
                    UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Forbidden"),
                    _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")

                };

                context.Response.StatusCode = statusCode;

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = statusCode == 500 ? "Ett oväntat fel uppstod på servern." : ex.Message,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
