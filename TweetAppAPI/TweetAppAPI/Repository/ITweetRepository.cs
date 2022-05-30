using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TweetAppAPI.Models;

namespace TweetAppAPI.Repository
{
    public interface ITweetRepository
    {
        List<Tweet> FindAll();
        Tweet FindByCondtion(Expression<Func<Tweet, bool>> expression);
        void Create(Tweet tweet);
        void Update(Tweet tweet);
        void Delete(string tweetId);
    }
}
