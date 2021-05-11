using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clipify.Domain.Entities;

namespace Clipify.Application.Common.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate);
    }
}