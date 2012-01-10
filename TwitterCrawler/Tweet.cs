using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Model
{    
    public class Tweet
    {        
        public decimal Id { get; set; }
        public string Username { get; set; }        
        public string ProfileImageURL { get; set; }
        public string Text { get; set; }
        public string Timestamp { get; set; }
        public long? NumberOfFollowers { get; set; }

        /// <summary>
        /// Side-effect free function. Extract URLs, hashtag and mention patterns inside a text and convert them to HTML links
        /// </summary>
        /// <param name="tweetText"></param>
        /// <returns></returns>
        public string FormatTweetText()
        {
            string formattedTweetText = string.Empty;
            formattedTweetText = Regex.Replace(this.Text, @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])",
               delegate(Match match)
               {
                   return string.Format("<a href=\"{0}\">{0}</a>", match.ToString());
               }, RegexOptions.IgnoreCase);

            formattedTweetText = Regex.Replace(formattedTweetText, @"#+[a-zA-Z0-9]*",
               delegate(Match match)
               {
                   return string.Format("<a href=\"http://twitter.com/#!/search?q={0}\">{1}</a>", match.ToString().Replace("#", "%23"), match.ToString());
               }, RegexOptions.IgnoreCase);

            formattedTweetText = Regex.Replace(formattedTweetText, @"@+[a-zA-Z0-9]*",
               delegate(Match match)
               {
                   string mention = match.ToString().Substring(1);
                   return string.Format("<a href=\"http://twitter.com/#!/{0}\">{1}</a>", mention, match.ToString());
               }, RegexOptions.IgnoreCase);

            return formattedTweetText;
        }
    }
}
