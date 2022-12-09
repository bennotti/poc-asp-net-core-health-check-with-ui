using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PocHealthCheckWithUi
{
    public class CustomHealthChecks : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var random = new Random();
            var list = new List<HealthStatus> { HealthStatus.Healthy, HealthStatus.Unhealthy, HealthStatus.Degraded };
            int index = random.Next(list.Count);

            return Task.FromResult(new HealthCheckResult(
                status: list[index],
                description: list[index] == HealthStatus.Healthy ? "Tudo certo" : list[index] == HealthStatus.Unhealthy ? "Algo errado não esta certo." : "Deu ruim"
            ));
        }
    }
}
