using Inbox.Core.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inbox.Sample.Caching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ICache _cache;

        public WeatherForecastController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var cacheKey = "Summaries";
            var cacheValue = _cache.Get<IEnumerable<WeatherForecast>>(cacheKey);
            if (cacheValue != null)
                return cacheValue;

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            _cache.TrySet<IEnumerable<WeatherForecast>>(cacheKey, result, TimeSpan.FromSeconds(10));
            return result;
        }
    }
}
