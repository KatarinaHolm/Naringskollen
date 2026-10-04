namespace Naringskollen.Middleware
{
    public sealed class RegistrationDisabledMiddleware
    {
        private readonly RequestDelegate next;

        public RegistrationDisabledMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.TrimEnd('/');
            if (HttpMethods.IsPost(context.Request.Method)
                && string.Equals(path, "/api/register", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            await next(context);
        }
    }
}
