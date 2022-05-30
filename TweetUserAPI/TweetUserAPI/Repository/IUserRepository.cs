using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TweetUserAPI.Models;

namespace TweetUserAPI.Repository
{
    public interface IUserRepository
    {
        List<User> FindAll();
        User FindByCondtion(Expression<Func<User, bool>> expression);
        void Create(User user);
        void Update(User user);
    }
}
