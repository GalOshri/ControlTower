using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ControlTower.BusinessLogic;

namespace ControlTower.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        // GET api/tweets/{tweet}
        [HttpGet("{tweet}")]
        public string Get(string tweet)
        {
            // In v1.1, all API calls require authentication
            
            /* 
            var service = new TwitterService("nCcoV35oxto4mCFPJPhHEzrTN", "5a52hIXJNxq7TCV8BaRHV08M4DK0YzvWyU9ZqlS2Rt1IGa4iV2");
            service.AuthenticateWith("3255256524-KYoXclTaiOOIhTcqEnliNoSyW6b8sAyaR9qSCD7", "nDKoIEbeec59cqbojVXPURfyTiSoT8LDOJxa4QFldsRq3");
            
            service.SendTweet(new SendTweetOptions { Status = tweet });
            return service.Response.Error.Message;
            */
            return tweet + tweet;
        }
        
        // GET api/tweets/{client}/{tweet}
        [HttpGet("{client}/{tweet}")]
        public string Get(string client, string tweet)
        {
            if (client == "weather")
            {
                return Weather.GetWeather(tweet);
            }
            else {
                Console.WriteLine(client);
                Console.WriteLine(tweet);
                return tweet + tweet;
            }
        }
	}
}
