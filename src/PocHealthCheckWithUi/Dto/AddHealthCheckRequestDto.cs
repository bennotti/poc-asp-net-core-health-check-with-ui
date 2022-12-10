namespace PocHealthCheckWithUi.Dto
{
    public class AddHealthCheckRequestDto
    {
        public string Nome { get; set; } = "PocHealthCheckAddServiceRuntime";
        public string Url { get; set; } = "https://localhost:7000/health";
    }
}
