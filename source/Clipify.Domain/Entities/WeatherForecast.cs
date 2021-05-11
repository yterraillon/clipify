using System;

namespace Clipify.Domain.Entities
{
    public class WeatherForecast
    {
        public WeatherForecast()
        {
            Summary = string.Empty;
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}