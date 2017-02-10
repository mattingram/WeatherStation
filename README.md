# WeatherStation

This project consists of a Raspberry Pi 3 running Windows IoT Core with a BME280 Temperature/Humidity/Pressure sensor hooked up.

WeatherSensor is an Universal Windows application built for IoT Core running on the Raspberry Pi that will POST data to an Azure web service.

WeatherService is a ASP.NET Core Web API app hosted in Azure that writes to a SqlLite database to store and serve the sensor data.
