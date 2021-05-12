using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clipify.Application.WeatherForecasts.Requests;
using Clipify.Application.WeatherForecasts.Requests.Models;

namespace Clipify.Infrastructure.WeatherForecasts
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate)
        {
            var random = new Random();

            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = random.Next(-20, 55),
                Summary = Summaries[random.Next(Summaries.Length)]
            }));

        }
    }
}