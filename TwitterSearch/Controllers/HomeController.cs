using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ApplicationLogic;
using TwitterSearch.Utility;
using Newtonsoft.Json;

namespace TwitterSearch.Controllers
{
    public class HomeController : Controller
    {
        TwitterAccessService service = new TwitterAccessService();
        bool singleTweetMode = true;
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // Ajax POST: /Home/StartSearch
        [HttpPost]
        public void StartSearch(string searchKey)
        {               
            this.service.Run(searchKey, string.Empty);            
        }

        //
        // Ajax POST: /Home/StopSearch
        [HttpPost]
        public void StopSearch()
        {
            this.service.Stop();
        }
                                         
        //
        // Ajax: /Home/GetTweets
        [HttpPost]
        public JsonNetResult GetTweets(decimal lastId)
        {
            JsonNetResult serializedTweets = new JsonNetResult();
            serializedTweets.Formatting = Formatting.Indented;
            if (singleTweetMode)
            {
                if (ViewBag.Count == null)
                {
                    ViewBag.Count = 0;
                }
                else
                {
                    ViewBag.Count++;
                }
                var tweet = service.GetTweet(lastId);
                if (tweet.Count == 0)
                {
                    tweet = null;
                }
                serializedTweets.Data = tweet;
            }
            else
            {
                if (ViewBag.Count == null)
                {
                    ViewBag.Count = 0;
                }
                else
                {
                    ViewBag.Count++;
                }
                var tweets = service.GetTweets(lastId);
                if (tweets.Count == 0)
                {
                    tweets = null;
                }
                serializedTweets.Data = tweets;
            }             
            return serializedTweets;
        }
          
    }
}
