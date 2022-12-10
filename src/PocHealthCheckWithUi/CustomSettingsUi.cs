using HealthChecks.UI.Configuration;

namespace PocHealthCheckWithUi
{
    public class CustomSettingsUi
    {
        public List<HealthCheckSetting> HealthChecks { get; set; } = new List<HealthCheckSetting>();
    }
}
