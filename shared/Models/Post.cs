namespace shared.Models
{
    public class Post
    {
        public long PostId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime PostTime { get; set; } = new DateTime();
        public string? UserName { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    
    public Post()
        {

        }

    public Post(string title = "", string body = "", int upVotes = 0, int downVotes = 0, DateTime postTime = new DateTime(), string userName = "")
        {
            this.Title = title;
            this.Body = body;
            this.UpVotes = upVotes;
            this.DownVotes = downVotes;
            this.PostTime = postTime;
            this.UserName = userName;
        }
    
      



    }
}
