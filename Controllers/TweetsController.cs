using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ControlTower.BusinessLogic;
using System.Net;

namespace ControlTower.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        // GET api/tweets?tweet=text
        [HttpGet()]
        public string Get()
        {
            string tweet = Request.Query["tweet"];
            tweet = WebUtility.UrlDecode(tweet);
            if (tweet.Contains("weather") || tweet.Contains("Weather"))
            {
                return Weather.GetWeatherFromTweet(tweet);
            }
            else if (tweet.Contains("translate") || tweet.Contains("Translate"))
            {
                return Translator.Translate(tweet);
            }
            else 
            {
                Console.WriteLine(tweet);
                return tweet + tweet;
            }
        }
	}
}
