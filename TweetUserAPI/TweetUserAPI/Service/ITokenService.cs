using TweetUserAPI.Models;

namespace TweetUserAPI.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
