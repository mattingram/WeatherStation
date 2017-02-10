using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private WeatherContext _context { get; set; }

        public WeatherController(WeatherContext context)
        {
            _context = context;
        }

        // GET api/weather
        [HttpGet]
        public IEnumerable<WeatherData> Get()
        {
            return _context.Weather.ToList();
        }

        // POST api/weather
        [HttpPost]
        public void Post([FromBody]WeatherData value)
        {
            _context.Add(new WeatherData {
                Temperature = value.Temperature,
                Humidity = value.Humidity,
                Pressure = value.Pressure,
                Altitude = value.Altitude
            });
            _context.SaveChanges();
        }
    }
}
