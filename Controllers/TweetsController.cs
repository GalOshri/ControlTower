using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using TweetSharp;

namespace ControlTower.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        // GET: api/tweets
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
	}
}
