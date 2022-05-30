
using System.Collections.Generic;
using TweetAppAPI.Models;
namespace TweetAppAPI.Service
{
    public interface ITweetService
    {
        public bool AddTweet(Tweet tweet);
        public List<Tweet> GetAllTweets();
        public List<Tweet> GetMyTweet(string username);
        public bool ReplyTweet(string tweetId,Reply reply);
        public bool UpdateTweet(string tweetId, Tweet tweet);
        public bool DeleteTweet(string tweetId);
        public bool LikeTweet(string tweetId);
    }
}
