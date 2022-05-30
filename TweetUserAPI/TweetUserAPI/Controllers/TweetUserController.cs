using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TweetUserAPI.DTO;
using TweetUserAPI.Models;
using TweetUserAPI.Service;


namespace TweetUserAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Tweets")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TweetUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TweetUserController(IUserService userService,ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("getAllUsers")]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("getUser/{username}")]
        public ActionResult<User> GetUser(string username)
        {
            return Ok(_userService.GetUser(username));
        }

        [HttpGet("getAllUsersnames")]
        public ActionResult<List<string>> GetAllUsersnames()
        {
            return Ok(_userService.GetAllUserNames());
        }

        [HttpGet("getAllEmails")]
        public ActionResult<List<string>> GetAllEmails()
        {
            return Ok(_userService.GetAllEmails());
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO login)
        {
            User user = _userService.Login(login);
            return Ok(_tokenService.GenerateToken(user));
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] User user)
        {
            return Ok(_userService.RegisterUser(user));
        }

        [HttpPost("forgotPassword")]
        public ActionResult ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            return Ok(_userService.ForgotPassword(forgotPassword));
        }

        [HttpPost("changePassword")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            return Ok(_userService.ChangePassword(changePasswordDTO));
        }

        [HttpPost("changePicture")]
        public ActionResult ChangePicture([FromBody] ChangePictureDTO changePictureDTO)
        {
            return Ok(_userService.ChangePicture(changePictureDTO));
        }
    }
}
