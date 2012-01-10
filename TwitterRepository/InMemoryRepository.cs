using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Repository
{
    public class InMemoryRepository : ITweetsRepository
    {
        private static List<Tweet> _tweets = new List<Tweet>();

        public void Add(Tweet status)
        {
            if (_tweets.Find(t => status.Id == t.Id) == null)
                _tweets.Add(status);
        }

        public void Delete(decimal id)
        {
            _tweets.Remove(_tweets.Find(t => t.Id == id));
        }

        public void Clear()
        {
            _tweets.Clear();
        }

        public List<Tweet> GetRange(int count, decimal lastId)
        {
            List<Tweet> displayTweets = new List<Tweet>();

            displayTweets = _tweets.Where(tweet => tweet.Id > lastId).ToList<Tweet>();
            if (displayTweets.Count > count)
            {
                displayTweets = displayTweets.GetRange(0, count);
            }
            displayTweets.Sort(new TwitterComparer());
            return displayTweets;
        }

        public Tweet GetStatus(decimal lastId)
        {
            Tweet status = null;
            if (_tweets.Count > 0)
            {
                //_tweets.Sort(new TwitterComparer());
                status = _tweets.Where(tweet => tweet.Id > lastId).First();
            }
            
            return status;
        }
    }
}
