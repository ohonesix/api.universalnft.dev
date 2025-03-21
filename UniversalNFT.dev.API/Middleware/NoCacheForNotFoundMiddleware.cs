namespace UniversalNFT.dev.API.Middleware
{
    public class NoCacheForNotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NoCacheForNotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.Headers.Remove("Cache-Control");
                    context.Response.Headers.Remove("Pragma");
                    context.Response.Headers.Remove("Expires");
                    context.Response.Headers.CacheControl = "no-store";
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
