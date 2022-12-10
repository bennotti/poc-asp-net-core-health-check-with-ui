using HealthChecks.UI.Configuration;
using HealthChecks.UI.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using PocHealthCheckWithUi.Dto;

namespace PocHealthCheckWithUi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableRateLimiting("fixed")]
    public class AdminHealthCheckController : ControllerBase
    {
        private readonly ILogger<AdminHealthCheckController> _logger;
        private readonly HealthChecksDb _healthChecksDb;

        public AdminHealthCheckController(
            ILogger<AdminHealthCheckController> logger,
            HealthChecksDb healthChecksDb
        ) {
            _logger = logger;
            _healthChecksDb = healthChecksDb;
        }

        [HttpGet]
        public IActionResult Lista()
        {
            var lista = new List<object>();

            if (_healthChecksDb != null)
            {
                lista.AddRange(_healthChecksDb.Configurations);
            }

            return Ok(new JsonResult(lista));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddHealthCheckRequestDto bodyRequest)
        {
            if (bodyRequest == null)
            {
                return BadRequest();
            }

            await _healthChecksDb.Configurations.AddAsync(new HealthCheckConfiguration { Name = bodyRequest.Nome, Uri = bodyRequest.Url });
            await _healthChecksDb.SaveChangesAsync();

            var lista = new List<object>();

            if (_healthChecksDb != null)
            {
                lista.AddRange(_healthChecksDb.Configurations);
            }

            return Ok(new JsonResult(lista));
        }
    }
}