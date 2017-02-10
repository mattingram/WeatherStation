using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WeatherService;

namespace WeatherService.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20170210052936_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("WeatherService.WeatherData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Altitude");

                    b.Property<DateTime>("DateAdded");

                    b.Property<double>("Humidity");

                    b.Property<double>("Pressure");

                    b.Property<double>("Temperature");

                    b.HasKey("Id");

                    b.ToTable("Weather");
                });
        }
    }
}
