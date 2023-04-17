using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using shared.Models;

namespace Data
{
    public class RedditContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();


        public RedditContext(DbContextOptions<RedditContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}