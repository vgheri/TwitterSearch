using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Twitterizer;

namespace ApplicationLogic
{
    class ModelTranslationService
    {
        public List<Tweet> ConvertToViewModel(List<TwitterStatus> statuses)
        {
            List<Tweet> tweets = new List<Tweet>();
            
            foreach (TwitterStatus status in statuses)
            {
                tweets.Add(ConvertToViewModel(status));
            }
            return tweets;
        }

        public Tweet ConvertToViewModel(TwitterStatus status)
        {            
            Tweet tweet;
            
            tweet = new Tweet()
            {
                Id = status.Id,
                Username = status.User.ScreenName,
                ProfileImageURL = status.User.ProfileImageLocation,
                Text = status.Text,
                Timestamp = status.CreatedDate.ToString(),
                NumberOfFollowers = status.User.NumberOfFollowers
            };

            tweet.Text = tweet.FormatTweetText();

            return tweet;
        }
    }
}
