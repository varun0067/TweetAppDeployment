using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TweetUserAPI.DatabaseConnection;
using TweetUserAPI.Exceptions;
using TweetUserAPI.Models;

namespace TweetUserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userData;

        public UserRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _userData = database.GetCollection<User>("UserData");
        }

        public void Create(User user)
        {
            try
            {
                _userData.InsertOne(user);
            }
            catch(Exception ex)
            {
                throw new InvalidDatabaseOperationException(ex.Message);
            }
        }

        public void Update(User user)
        {
            try
            {
                _userData.ReplaceOne(x => x.Email == user.Email, user);
            }
            catch (Exception ex)
            {

                throw new InvalidDatabaseOperationException(ex.Message);
            }
        }

        public List<User> FindAll()
        {
            return _userData.Find(user => true).ToList();
        }

        public User FindByCondtion(Expression<Func<User, bool>> expression)
        {
            return _userData.Find(expression).FirstOrDefault();
        }

    }
}
