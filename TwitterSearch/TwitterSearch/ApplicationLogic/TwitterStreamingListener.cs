using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Twitterizer;
using Twitterizer.Streaming;
using Model;
using Repository;

namespace ApplicationLogic
{
    public class TwitterStreamingListener
    {
        private static TwitterStreamingListener _instance = null;
        private ITweetsRepository _repository;
        private List<string> _otherEvents;
        private bool _stopSearch;
        private ModelTranslationService _translationService;
        static OAuthTokens tokens = new OAuthTokens()
        {
            ConsumerKey = "yourConsumerKey",
            ConsumerSecret = "yourConsumerKeySecret",
            AccessToken = "yourAccessToken",
            AccessTokenSecret = "yourAccessTokenSecret"
        };
        

        public static TwitterStreamingListener GetInstance(ITweetsRepository repository)
        {
            if (_instance == null)
            {
                _instance = new TwitterStreamingListener(repository);
            }
            return _instance;
        }

        private TwitterStreamingListener(ITweetsRepository injected)
        {            
            this._stopSearch = false;
            this._repository = injected;
            this._otherEvents = new List<string>();
            this._translationService = new ModelTranslationService();
        }              

        public void StartCaptureStreaming(SearchParameters parameters)
        {
            StreamOptions options = new StreamOptions();
            options.Track.Add(parameters.SearchKey);
            this._stopSearch = false;
                        
            TwitterStream stream = new TwitterStream(tokens, "RTSearch (Dev)", options);
            
            IAsyncResult result = stream.StartPublicStream(
                StreamStopped,
                NewTweet,
                DeletedTweet,
                OtherEvent
            );
            
            while (!this._stopSearch)
            {

            }
            
            stream.EndStream(StopReasons.StoppedByRequest, "Stop by user");
        }

        public void StopCaptureStreaming()
        {
            this._stopSearch = true;
        } 
                
        void StreamStopped(StopReasons reason)
        {
            if (reason == StopReasons.StoppedByRequest)
            {
                this._repository.Clear();
                this._stopSearch = true;                          
            }
            else
            {
                //Do something...
            }
        }
                
        void NewTweet(TwitterStatus twitterizerStatus)
        {
            Tweet tweet = this._translationService.ConvertToViewModel(twitterizerStatus);
            this._repository.Add(tweet);
        }

        void DeletedTweet(TwitterStreamDeletedEvent e)
        {
            this._repository.Delete(e.Id);
        }

        void OtherEvent(TwitterStreamEvent e)
        {
            this._otherEvents.Add(e.EventType);            
        }        
    }
}
