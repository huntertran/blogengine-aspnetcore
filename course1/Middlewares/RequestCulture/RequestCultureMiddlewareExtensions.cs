namespace course1.Middlewares.RequestCulture
{
    using course1.Middlewares.RequestCulture;
    using Microsoft.AspNetCore.Builder;

    // Expose through IApplicationBuilder
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}