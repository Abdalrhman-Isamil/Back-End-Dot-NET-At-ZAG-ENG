using System.Diagnostics;

namespace JobListingsAPI.Middleware
{
    /// <summary>
    /// Custom middleware that logs every HTTP request and response to the console.
    /// Format: [yyyy-MM-dd HH:mm:ss] METHOD /path → STATUS ReasonPhrase (took Xms)
    /// </summary>
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // --- BEFORE the request runs ---
            var method    = context.Request.Method;
            var path      = context.Request.Path;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine($"[{timestamp}] {method} {path} → (processing...)");

            // Pass control to the next middleware / endpoint
            await _next(context);

            // --- AFTER the response has been written ---
            stopwatch.Stop();
            var statusCode   = context.Response.StatusCode;
            var reasonPhrase = GetReasonPhrase(statusCode);
            var duration     = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"[{timestamp}] {method} {path} → {statusCode} {reasonPhrase} (took {duration}ms)");
        }

        private static string GetReasonPhrase(int statusCode) => statusCode switch
        {
            200 => "OK",
            201 => "Created",
            204 => "No Content",
            400 => "Bad Request",
            404 => "Not Found",
            500 => "Internal Server Error",
            _   => string.Empty
        };
    }

    // Extension method for clean registration in Program.cs
    public static class RequestLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder app)
            => app.UseMiddleware<RequestLoggerMiddleware>();
    }
}
