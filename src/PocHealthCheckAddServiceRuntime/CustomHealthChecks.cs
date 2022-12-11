using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PocHealthCheckAddServiceRuntime
{
    public class CustomHealthChecks : IHealthCheck
    {
        private readonly Random random = new Random();
        private IList<HealthStatus> list = new List<HealthStatus> { HealthStatus.Healthy, HealthStatus.Unhealthy, HealthStatus.Degraded };
        private int index = 0;
        private DateTime proximaMudanca = DateTime.UtcNow;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow >= proximaMudanca) {
                index = random.Next(list.Count);
                proximaMudanca = DateTime.UtcNow.AddMinutes(5);
            }

            return Task.FromResult(new HealthCheckResult(
                status: list[index],
                description: list[index] == HealthStatus.Healthy ? "Tudo certo" : list[index] == HealthStatus.Unhealthy ? "Algo errado não esta certo." : "Deu ruim"
            ));
        }
    }
}
