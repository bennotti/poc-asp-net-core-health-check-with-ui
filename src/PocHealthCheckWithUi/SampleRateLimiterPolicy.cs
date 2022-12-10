using k8s.KubeConfigModels;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

namespace PocHealthCheckWithUi
{
    public class SampleRateLimiterPolicy : IRateLimiterPolicy<string>
    {
        private Func<OnRejectedContext, CancellationToken, ValueTask>? _onRejected;

        public SampleRateLimiterPolicy(ILogger<SampleRateLimiterPolicy> logger)
        {
            _onRejected = (ctx, token) =>
            {
                ctx.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                ctx.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Status = "Unhealthy",
                    TotalDuration = "00:00:00.0000000",
                });
                logger.LogWarning($"Request rejected by {nameof(SampleRateLimiterPolicy)}");
                return ValueTask.CompletedTask;
            };
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => _onRejected;

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetSlidingWindowLimiter(string.Empty,
                _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 1,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0,
                    Window = TimeSpan.FromSeconds(10),
                    SegmentsPerWindow = 5
                });
        }
    }
}
