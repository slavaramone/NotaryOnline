using Microsoft.AspNetCore.Builder;
using SharedLib.Middleware;

namespace SharedLib.Extensions
{
	public static class ExceptionHandlingMiddlewareExtension
	{
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
