using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TweetAppAPI.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string TweetMessage { get; set; }
        public string TweetTags { get; set; }
        public DateTime TweetTime { get; set; }
        public List<Reply> Replies { get; set; }
        public int Likes { get; set; } = 0;
    }
}
