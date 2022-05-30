using System;

namespace TweetUserAPI.DTO
{
    public class ForgotPasswordDTO
    {
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NewPassword { get; set; }
    }
}
