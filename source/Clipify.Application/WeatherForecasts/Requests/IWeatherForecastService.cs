using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clipify.Application.WeatherForecasts.Requests.Models;

namespace Clipify.Application.WeatherForecasts.Requests
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate);
    }
}