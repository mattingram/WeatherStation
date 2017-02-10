using System;
using Microsoft.EntityFrameworkCore;

namespace WeatherService
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherData> Weather { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("filename={0}/Weather.db", AppContext.BaseDirectory));
        }
    }

    public class WeatherData
    {
        public WeatherData()
        {
            DateAdded = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double Altitude { get; set; }
    }
}
