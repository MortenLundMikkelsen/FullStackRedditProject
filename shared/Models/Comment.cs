namespace shared.Models
{
    public class Comment
    {
        public long CommentId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime PostTime { get; set; } =  new DateTime();
        public string? UserName { get; set; }
        public long PostId { get; set; }
        public Post? Post { get; set; }

        public Comment()
        {

        }
        public Comment(string title = "", string body = "", int upVotes = 0, int downVotes = 0, DateTime postTime = new DateTime(), string userName = "")
        {
            this.Title = title;
            this.Body = body;
            this.UpVotes = upVotes;
            this.DownVotes = downVotes;
            this.PostTime = postTime;
            this.UserName = userName;
            
        }

        public Comment(string title = "", string body = "", int upVotes = 0, int downVotes = 0, DateTime postTime = new DateTime(), string userName = "", long postId = 1)
        {
            this.Title = title;
            this.Body = body;
            this.UpVotes = upVotes;
            this.DownVotes = downVotes;
            this.PostTime = postTime;
            this.UserName = userName;
            this.PostId = postId;

        }


        public Comment(string title = "", string body = "", int upVotes = 0, int downVotes = 0, DateTime postTime = new DateTime(), string userName = "", Post post = null)
        {
            this.Title = title;
            this.Body = body;
            this.UpVotes = upVotes;
            this.DownVotes = downVotes;
            this.PostTime = postTime;
            this.UserName = userName;
            this.Post = post;

        }


    }
}
