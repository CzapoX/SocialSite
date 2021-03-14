using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLiker> PostLikers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostLiker>(x => x.HasKey(a => new { a.AppUserId, a.PostId }));

            builder.Entity<PostLiker>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.PostsLiked)
                .HasForeignKey(x => x.AppUserId);

            builder.Entity<PostLiker>()
               .HasOne(x => x.Post)
               .WithMany(x => x.PostLikers)
               .HasForeignKey(x => x.PostId);

            builder.Entity<Comment>()
                .HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
