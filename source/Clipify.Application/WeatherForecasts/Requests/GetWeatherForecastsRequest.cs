using Clipify.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common.Interfaces;

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