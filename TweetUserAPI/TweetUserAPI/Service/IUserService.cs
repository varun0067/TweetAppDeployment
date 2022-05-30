using System.Collections.Generic;
using TweetUserAPI.DTO;
using TweetUserAPI.Models;

namespace TweetUserAPI.Service
{
    public interface IUserService
    {
        public bool RegisterUser(User user);
        public User Login(LoginDTO loginUser);
        public User GetUser(string username);
        public bool ForgotPassword(ForgotPasswordDTO forgotPassword);
        public bool ChangePassword(ChangePasswordDTO changePassword);
        public bool ChangePicture(ChangePictureDTO changePassword);
        public List<User> GetAllUsers();
        public List<string> GetAllUserNames();
        public List<string> GetAllEmails();
    }
}
