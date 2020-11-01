using Microsoft.AspNetCore.Mvc;
using MyMicroserviceActio.Common.Commands;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace MyMicroserviceActio.Api.Controllers
{
    [Route("[controller]")]
    public class ActivitiesController: Controller
    {
        private readonly IBusClient _busClient;
        public ActivitiesController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity commad)
        {
            commad.Id = Guid.NewGuid();
            commad.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(commad);

            return Accepted($"activities/{commad.Id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            await Task.CompletedTask;
            return Ok($"Activity Created, id= " + id);
        }
    }
}
