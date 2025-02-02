using Microsoft.AspNetCore.Http; 

namespace TSP.Application.Middleware;

public class RequestCleanupMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCleanupMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        finally
        {
            if (context.RequestAborted.IsCancellationRequested)
            {
                // Force cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}