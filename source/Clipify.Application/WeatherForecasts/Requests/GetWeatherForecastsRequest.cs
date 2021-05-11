using Clipify.Application.Common.Interfaces;
using Clipify.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.WeatherForecasts.Requests
{
    public static class GetWeatherForecasts
    {
        public class Request : IRequest<IEnumerable<WeatherForecast>>
        {
            public DateTime StartDate { get; set; }
        }

        public class Handler : IRequestHandler<Request, IEnumerable<WeatherForecast>>
        {
            private readonly IWeatherForecastService _weatherForecastService;

            public Handler(IWeatherForecastService weatherService)
            {
                _weatherForecastService = weatherService;
            }

            public Task<IEnumerable<WeatherForecast>> Handle(Request request, CancellationToken cancellationToken)
            {
                return _weatherForecastService.GetWeatherForecasts(request.StartDate);
            }
        }
    }
}