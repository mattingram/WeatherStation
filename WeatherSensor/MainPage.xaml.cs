using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherSensor
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		//A class which wraps the barometric sensor
		BME280Sensor BME280;
		static HttpClient client = new HttpClient()
		{
			BaseAddress = new Uri("http://gliding.azurewebsites.net/api/weather")
		};

		public MainPage()
		{
			this.InitializeComponent();
		}

		// This method will be called by the application framework when the page is first loaded
		protected override async void OnNavigatedTo(NavigationEventArgs navArgs)
		{
			try
			{
				// Create a new object for our sensor class
				BME280 = new BME280Sensor();
				//Initialize the sensor
				await BME280.Initialize();

				//Create variables to store the sensor data: temperature, pressure, humidity and altitude. 
				//Initialize them to 0.
				float temp = 0;
				float pressure = 0;
				float altitude = 0;
				float humidity = 0;

				//Create a constant for pressure at sea level. 
				//This is based on your local sea level pressure (Unit: Hectopascal)
				//TODO: read current sea Level Pressure from web service
				const float seaLevelPressure = 1003.00f;

				//Read 10 samples of the data
				for (int i = 0; i < 10; i++)
				{
					temp = await BME280.ReadTemperature();
					pressure = await BME280.ReadPreasure();
					altitude = await BME280.ReadAltitude(seaLevelPressure);
					humidity = await BME280.ReadHumidity();

					//Write the values to your debug console
					Debug.WriteLine("Temperature: " + temp.ToString());
					Debug.WriteLine("Humidity: " + humidity.ToString() + " %");
					Debug.WriteLine("Pressure: " + pressure.ToString() + " Pa");
					Debug.WriteLine("Altitude: " + altitude.ToString() + " m");
					Debug.WriteLine("");

					await PostWeatherDataAsync(temp, humidity, pressure, altitude);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
		
		private async Task PostWeatherDataAsync(float temp, float humidity, float pressure, float altitude)
		{
			try
			{
				var data = new WeatherData
				{
					Temperature = temp,
					Humidity = humidity,
					Pressure = pressure,
					Altitude = altitude
				};

				var response = await client.PostAsJsonAsync("", data);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Web call failed: " + e.Message);
			}
		}
	}
}
