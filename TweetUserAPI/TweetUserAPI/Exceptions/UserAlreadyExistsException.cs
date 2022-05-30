using System;

namespace TweetUserAPI.Exceptions
{
    public class UserAlreadyExistsException:ApplicationException
    {
        public UserAlreadyExistsException()
        {

        }

        public UserAlreadyExistsException(string msg):base(msg)
        {

        }
    }
}
