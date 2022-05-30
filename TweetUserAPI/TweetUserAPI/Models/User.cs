using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace TweetUserAPI.Models
{
    public class User
    {
            [BsonId]
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            [JsonIgnore]
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
            public long ContactNumber { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
            public string ProfileImage { get; set; }
    }
}
