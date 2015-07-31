using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ControlTower.BusinessLogic
{
	public class Weather 
	{
		private class CityWeather 
		{
			public double temp;
			public string description;
			public string city;
			
			public CityWeather(double temp, string description, string city)
			{
				this.temp = temp;
				this.description = description;
				this.city = city;
			}
		}
		public static string GetWeatherFromTweet(string tweet)
		{	
			string city = GetCityFromTweet(tweet);
			
			Console.WriteLine(city);
			
			CityWeather cityWeather = GetWeatherFromCity(city);
			
			return string.Format("The weather in {0}: {1} and {2}°F", cityWeather.city, cityWeather.description, cityWeather.temp);			
		}
		
		private static string GetCityFromTweet(string tweet)
		{
			int cityStartIndex = tweet.IndexOf("in ") + "in ".Length;
			int cityEndIndex = tweet.LastIndexOf("?");
			
			string city;
			if (cityEndIndex == -1)
			{
				city = tweet.Substring(cityStartIndex);
			}
			else
			{
				city = tweet.Substring(cityStartIndex, cityEndIndex - cityStartIndex);
			}
			
			return city;
		}
		
		private static CityWeather GetWeatherFromCity(string city)
		{
			city = WebUtility.UrlEncode(city);
			string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=imperial", city);
			WebRequest request = WebRequest.Create(url);
			WebResponse response = request.GetResponse();
			
			using (var streamReader = new StreamReader(response.GetResponseStream()))
			{
				string responseString = streamReader.ReadToEnd();
				
				Console.WriteLine(responseString);
				JObject joResponse = JObject.Parse(responseString);
				JObject main = (JObject)joResponse["main"];
				double temp = (double) main["temp"];
				JObject weather = (JObject)joResponse["weather"][0];
				string description = (string) weather["description"];
				string cityName = (string) joResponse["name"];
				
				Console.WriteLine(string.Format("temp is: {0}", temp));
				CityWeather cityWeather = new CityWeather(temp, description, cityName);
				
				return cityWeather;
			}
		}
	}
}