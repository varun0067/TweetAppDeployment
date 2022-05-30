using System.Collections.Generic;
using System.Linq;
using TweetUserAPI.DTO;
using TweetUserAPI.Exceptions;
using TweetUserAPI.Models;
using TweetUserAPI.Repository;

namespace TweetUserAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ChangePassword(ChangePasswordDTO changePassword)
        {
            User user = _userRepository.FindByCondtion(u => (u.Email.Equals(changePassword.Username) || u.Username == changePassword.Username));
            if (user != null && BCrypt.Net.BCrypt.Verify(changePassword.Password, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
                _userRepository.Update(user);
                return true;
            }
            else
                throw new InvalidCredentialsException("Invalid Credentials");

        }
        public bool ChangePicture(ChangePictureDTO changePicture)
        {
            User user = _userRepository.FindByCondtion(u => (u.Email.Equals(changePicture.Username) || u.Username == changePicture.Username));

            user.ProfileImage = changePicture.ProfileImage;
            _userRepository.Update(user);
            return true;

        }


        public bool ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            User user = _userRepository.FindByCondtion(u => (u.Email.Equals(forgotPassword.Username) || u.Username == forgotPassword.Username));

            if (user != null && user.DateOfBirth.ToString("MM/dd/yyyy").Equals(forgotPassword.DateOfBirth.ToString("MM/dd/yyyy")))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(forgotPassword.NewPassword);
                _userRepository.Update(user);
                return true;
            }
            else
                throw new InvalidCredentialsException("Invalid Credentials");
        }

        public List<string> GetAllEmails()
        {
            return _userRepository.FindAll().Select(x => x.Email).ToList();
        }

        public List<string> GetAllUserNames()
        {
            return _userRepository.FindAll().Select(x => x.Username).ToList();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.FindAll();
        }

        public User GetUser(string username)
        {
            return _userRepository.FindByCondtion(u => (u.Email.Equals(username) || u.Username == username));
        }

        public User Login(LoginDTO loginUser)
        {
            User user = _userRepository.FindByCondtion(u => (u.Email.Equals(loginUser.Username) || u.Username == loginUser.Username));
            if (user != null && BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return user;
            }
            else
                throw new InvalidCredentialsException("Invalid Credentials");
        }

        public bool RegisterUser(User user)
        {
            User existingUserWithEmail = _userRepository.FindByCondtion(u => u.Email.Equals(user.Email));
            User existingUserWithUsername = _userRepository.FindByCondtion(u => u.Username.Equals(user.Username));

            if (existingUserWithEmail == null && existingUserWithUsername == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _userRepository.Create(user);
                return true;
            }
            else
            {
                if (existingUserWithEmail != null)
                    throw new UserAlreadyExistsException($"User with email {user.Email} already exists");
                else
                    throw new UserAlreadyExistsException($"User with username {user.Username} already exists");
            }
        }

    }
}
