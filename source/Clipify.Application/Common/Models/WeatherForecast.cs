using System;

<<<<<<< HEAD:source/Clipify.Domain/Entities/WeatherForecast.cs
namespace Clipify.Domain.Entities
=======
namespace Clipify.Application.Common.Models
>>>>>>> main:source/Clipify.Application/Common/Models/WeatherForecast.cs
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

        public string Summary { get; set; } = string.Empty;
    }
}