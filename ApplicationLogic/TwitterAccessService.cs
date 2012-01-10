using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Repository;
using Twitterizer;
using System.Text.RegularExpressions;

namespace ApplicationLogic
{
    public class TwitterAccessService
    {
        const int COUNT = 50;
        ITweetsRepository repository;
        IRepositoryFactory factory;
        TwitterStreamingListener listener;

        public TwitterAccessService()
        {
            factory = new RepositoryFactory();
            repository = factory.GetTwitterRepository();
            listener = TwitterStreamingListener.GetInstance(repository);
        }

        public void Run(string searchKey, string location)
        {            
            SearchParameters parameters = new SearchParameters()
            {
                SearchKey = searchKey,
                Location = location,
                Count = string.Empty
            };

            listener.StartCaptureStreaming(parameters);
        }

        public void Stop()
        {
            listener.StopCaptureStreaming();
        }

        public List<Tweet> GetTweets(decimal lastId)
        {
            List<Tweet> tweets = new List<Tweet>();
            tweets  = repository.GetRange(COUNT, lastId);            
            return tweets;
        }

        /// <summary>
        /// In order to remain complying with the User Interface, that expects a list of Tweets, we return a list of Tweets
        /// </summary>
        /// <returns>A list of tweets with only one Twitter status</returns>
        public List<Tweet> GetTweet(decimal lastId)
        {
            List<Tweet> twitterStatuses = new List<Tweet>();
            Tweet status = repository.GetStatus(lastId);
            twitterStatuses.Add(status);
            return twitterStatuses;
        }
    }
}