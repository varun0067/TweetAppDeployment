using System;

namespace TweetUserAPI.Exceptions
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
