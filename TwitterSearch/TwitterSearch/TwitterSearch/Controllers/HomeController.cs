using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ApplicationLogic;

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
        public ActionResult GetTweets(decimal lastId)
        {
            JsonResult serializedTweets;
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
                serializedTweets = Json(tweet);
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
                serializedTweets = Json(tweets);
            }             
            return serializedTweets;
        }
          
    }
}
