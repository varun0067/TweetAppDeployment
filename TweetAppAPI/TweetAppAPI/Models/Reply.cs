using System;

namespace TweetAppAPI.Models
{
    public class Reply
    {
        public string Username { get; set; }
        public string TweetMessage { get; set; }
        public string TweetTags { get; set; }
        public DateTime TweetTime{ get; set; }
    }
}
