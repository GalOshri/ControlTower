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
		public static string GetWeather(string city)
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
				return string.Format("Temperature is: {0}°F", temp);
			}
			
			
			
			
			
		}
	}
}