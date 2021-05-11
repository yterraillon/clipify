using Clipify.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clipify.Application.Common.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate);
    }
}