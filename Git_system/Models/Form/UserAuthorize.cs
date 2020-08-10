namespace Git_system.Models.Form
{
    public class UserAuthorize
    {
        public UserAuthorize()
        {
        }

        public UserAuthorize(int id, string username, string type)
        {
            this.Id = id;
            this.Username = username;
            this.Type = type;
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Type { get; set; }
    }
}