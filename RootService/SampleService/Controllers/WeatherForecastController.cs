using Microsoft.AspNetCore.Mvc;
using RootServiceNamespace;
using SampleService.Services;

namespace SampleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      //  private readonly  RootServiceClient _rootServiceClient;
        private readonly ILogger<WeatherForecastController> _logger;
        private IRootServiceClient _rootServiceClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRootServiceClient rootServiceClient)
        {
            
            _logger = logger;
            _rootServiceClient = rootServiceClient;
            //_rootServiceClient = new RootServiceClient("http://localhost:5226", httpClientFactory.CreateClient("RootServiceClient"));
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<RootServiceReference.WeatherForecast>>> Get()
         {
                return Ok(await _rootServiceClient.Get());
         }
   
    }
}