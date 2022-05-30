using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TweetAppAPI.Models;
using TweetAppAPI.Service;

namespace TweetAppAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Tweets")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "User")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpPost("addTweet")]
        public ActionResult AddTweet([FromBody] Tweet tweet)
        {
            return Ok(_tweetService.AddTweet(tweet));
        }

        [HttpGet("getAllTweets")]
        public ActionResult<List<Tweet>> GetAllTweets()
        {
            return Ok(_tweetService.GetAllTweets());
        }

        [HttpGet("getMyTweets/{username}")]
        public ActionResult<List<Tweet>> GetMyTweets(string username)
        {
            return Ok(_tweetService.GetMyTweet(username));
        }

        [HttpPost("replyTweet/{tweetId}")]
        public ActionResult ReplyTweet(string tweetId,[FromBody] Reply reply)
        {
            return Ok(_tweetService.ReplyTweet(tweetId,reply));
        }

        [HttpPut("updateTweet/{tweetId}")]
        public ActionResult UpdateTweet(string tweetId, [FromBody] Tweet tweet)
        {
            return Ok(_tweetService.UpdateTweet(tweetId, tweet));
        }
        
        [HttpDelete("deleteTweet/{tweetId}")]
        public ActionResult DeleteTweet(string tweetId)
        {
            return Ok(_tweetService.DeleteTweet(tweetId));
        }

        [HttpPut("likeTweet/{tweetId}")]
        public ActionResult LikeTweet(string tweetId)
        {
            return Ok(_tweetService.LikeTweet(tweetId));
        }

    }
}
