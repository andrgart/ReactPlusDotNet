using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.AppIdentity;
using WebApiTest.Dto;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost]
        [Route("test_post")]
        public ActionResult<OlegDto> Post([FromBody] OlegDto model)
        {
            if (model == null)
            {
                Console.WriteLine("oleg is bad");
                return BadRequest();
            }

            var oleg = new OlegDto
            {
                Name = model.Name,
                Age = model.Age
            };

            Console.WriteLine($"this human is {oleg.Name} and it has {oleg.Age} years behind");

            return Ok(oleg);
        }
    }
}
