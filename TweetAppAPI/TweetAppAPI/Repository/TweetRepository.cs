using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TweetAppAPI.DatabaseConnection;
using TweetAppAPI.Exceptions;
using TweetAppAPI.Models;

namespace TweetAppAPI.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IMongoCollection<Tweet> _tweetData;

        public TweetRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetData = database.GetCollection<Tweet>("TweetData");
        }
        public void Create(Tweet tweet)
        {
            try
            {
                _tweetData.InsertOne(tweet);
            }
            catch (Exception ex)
            {
                throw new InvalidDatabaseOperationException(ex.Message);
            }
        }

        public void Delete(string tweetId)
        {
            try
            {
                _tweetData.DeleteOne(tweet=>tweet.Id==tweetId);
            }
            catch (Exception ex)
            {
                throw new InvalidDatabaseOperationException(ex.Message);
            }
        }

        public List<Tweet> FindAll()
        {
            return _tweetData.Find(user => true).ToList();
        }

        public Tweet FindByCondtion(Expression<Func<Tweet, bool>> expression)
        {
            return _tweetData.Find(expression).FirstOrDefault();
        }

        public void Update(Tweet tweet)
        {
            try
            {
                _tweetData.ReplaceOne(t => t.Id == tweet.Id, tweet);
            }
            catch (Exception ex)
            {

                throw new InvalidDatabaseOperationException(ex.Message);
            }
        }
    }
}
