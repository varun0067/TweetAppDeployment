using System.Collections.Generic;
using System.Linq;
using TweetAppAPI.Models;
using TweetAppAPI.Repository;

namespace TweetAppAPI.Service
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public bool AddTweet(Tweet tweet)
        {
            _tweetRepository.Create(tweet);
            return true;
        }

        public bool DeleteTweet(string tweetId)
        {
            _tweetRepository.Delete(tweetId);
            return true;
        }

        public bool LikeTweet(string tweetId)
        {
            Tweet tweet = _tweetRepository.FindByCondtion(t => t.Id == tweetId);

            tweet.Likes += 1;

            _tweetRepository.Update(tweet);
            return true;
        }

        public List<Tweet> GetAllTweets()
        {
            List<Tweet> tweets=_tweetRepository.FindAll();
            tweets.Reverse();
            return tweets;
        }

        public List<Tweet> GetMyTweet(string username)
        {
            List<Tweet> tweets = _tweetRepository.FindAll();
            tweets.Reverse();

            return (from tweet in tweets
                   where tweet.Username == username
                   select tweet).ToList();
        }

        public bool ReplyTweet(string tweetId, Reply reply)
        {
            Tweet tweet = _tweetRepository.FindByCondtion(t => t.Id == tweetId);

            tweet.Replies.Add(reply);

            _tweetRepository.Update(tweet);
            return true;
        }

        public bool UpdateTweet(string tweetId, Tweet tweet)
        {
            Tweet existingTweet = _tweetRepository.FindByCondtion(t => t.Id == tweetId);

            _tweetRepository.Update(existingTweet);
            return true;
        }
    }
}
