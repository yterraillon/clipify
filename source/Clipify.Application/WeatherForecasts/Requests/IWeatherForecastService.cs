using Clipify.Application.WeatherForecasts.Requests.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipify.Application.WeatherForecasts.Requests
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate);
    }
}