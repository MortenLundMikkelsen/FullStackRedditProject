using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using shared.Models;
using System.Linq;
using Data;

namespace miniprojektsem3.Service;

public class RedditService
{
    private RedditContext db { get; }

    public RedditService(RedditContext db)
    {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {

        Post post = db.Posts.FirstOrDefault()!;

        if (post == null)
        { 
            Post post1 = new Post("hamster", "hamster", 0, 0, DateTime.Now, "Hans");
            db.Posts.Add(post1);
            

            Post post2 = new Post("Kanin", "Kanin", 0, 0, DateTime.Now, "Petyr Plus");
            db.Posts.Add(post2);


            db.Comments.Add(new Comment("Tag dig sammen", "hold den gode tone, tak", 0, 0, DateTime.Now, "Cornelius Prosa", post1));
            db.Comments.Add(new Comment("Slap af Prosa", "Jeg synes faktisk du burde lukke røven", 0, 0, DateTime.Now, "Petyr Plus", post1));
            db.Comments.Add(new Comment("Rolig drenge", "Skal vi ikke bare være venner?", 0, 0, DateTime.Now, "Hans", post2));

        }



        db.SaveChanges();
    }

    // Henter alle post
    public List<Post> GetAllPosts()
    {
        return db.Posts.ToList();
    }

    // Henter specifik post på id
    public Post GetPost(int id)
    {
        return db.Posts.Where(p => p.PostId == id).Include(p => p.Comments).FirstOrDefault();

    }


    // Henter alle comments på specifik post

    public List<Comment> GetComments(long postId)
    {
        Post post = db.Posts.FirstOrDefault(p => p.PostId == postId);
        return db.Comments.Where(c => c.Post.PostId == post.PostId).ToList();
    }



    // Opretter Post
    public Post CreatePost(string title, string body, string username )
    {
        Post newpost = new Post(title,body, 0, 0 , DateTime.Now,username );
        db.Add(newpost);
        db.SaveChanges();
        return newpost;
        

    }


    // Opretter Comment på tilhørende post

    public Comment CreateComment(string title, string body, string userName, long postId)
    {
        Post post = db.Posts.FirstOrDefault(p => p.PostId == postId);
        Comment newComment = new Comment(title, body, 0, 0, DateTime.Now, userName, postId);
        db.Add(newComment);
        db.SaveChanges();
        return newComment;
    }



    // Tilføjer Upvote på tilhørende post

    public string UpVotePost(long postId)
    {
        Post post = db.Posts.Where(p => p.PostId == postId).FirstOrDefault()!;
        post.UpVotes++;
        db.SaveChanges();
        return "Post upvoted";
    }


    // Tilføjer downvote på tilhørende post

    public string DownVotePost(long postId)
    {
        Post post = db.Posts.Where(p => p.PostId == postId).FirstOrDefault()!;
        post.DownVotes++;
        db.SaveChanges();
        return "Post downvoted";

    }

 
    // Tilføjer Upvote på comment på Comment

    public string UpVoteComment(long commentId)
    {
        Comment comment = db.Comments.Where(c => c.CommentId == commentId).FirstOrDefault()!;
        comment.UpVotes++;
        db.SaveChanges();
        return "Comment upvoted";
    }


    // Tilføjer downvote på tilhørende Comment

    public string DownVoteComment(long commentId)
    {
        Comment comment = db.Comments.Where(c => c.CommentId == commentId).FirstOrDefault()!;
        comment.DownVotes++;
        db.SaveChanges();
        return "Comment downvoted";



    }


}