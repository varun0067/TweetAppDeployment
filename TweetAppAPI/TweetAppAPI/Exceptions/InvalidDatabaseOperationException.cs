using System;

namespace TweetAppAPI.Exceptions
{
    public class InvalidDatabaseOperationException:ApplicationException
    {
        public InvalidDatabaseOperationException()
        {

        }

        public InvalidDatabaseOperationException(string msg):base(msg)
        {

        }
    }
}
