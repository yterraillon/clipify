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
        public class GetWeatherForecastsRequest : IRequest<IEnumerable<WeatherForecast>>
        {
            public DateTime StartDate { get; set; }
        }

        public class GetWeatherForecastsRequestHandler : IRequestHandler<GetWeatherForecastsRequest, IEnumerable<WeatherForecast>>
        {
            private readonly IWeatherForecastService _weatherForecastService;

            public GetWeatherForecastsRequestHandler(IWeatherForecastService weatherService)
            {
                _weatherForecastService = weatherService;
            }

            public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsRequest request, CancellationToken cancellationToken)
            {
                return _weatherForecastService.GetWeatherForecasts(request.StartDate);
            }
        }
    }
}