namespace TweetAppAPI.DatabaseConnection
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
