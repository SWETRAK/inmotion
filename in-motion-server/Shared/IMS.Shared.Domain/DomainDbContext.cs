using IMS.Shared.Domain.Entities.Other;
using IMS.Shared.Domain.Entities.Post;
using IMS.Shared.Domain.Entities.User;
using IMS.Shared.Domain.EntityPreferences;
using IMS.Shared.Domain.EntityPreferences.Other;
using IMS.Shared.Domain.EntityPreferences.Post;
using IMS.Shared.Domain.EntityPreferences.User;
using Microsoft.EntityFrameworkCore;

namespace IMS.Shared.Domain;

public class DomainDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfileVideo> UserProfileVideos { get; set; } 
    public DbSet<Provider> Providers { get; set; }
    public DbSet<UserProfileVideoReaction> UserProfileVideoReactions { get; set; }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostVideo> PostVideos { get; set; }
    public DbSet<PostBaseComment> PostComments { get; set; }
    public DbSet<PostReaction> PostReactions { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Localization> Localizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=inMotion;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
        new ProviderEntityConfiguration().Configure(modelBuilder.Entity<Provider>());
        new UserProfileVideoEntityConfiguration().Configure(modelBuilder.Entity<UserProfileVideo>());
        new UserProfileVideoReactionEntityConfiguration().Configure(modelBuilder.Entity<UserProfileVideoReaction>());
        
        new LocalizationEntityConfiguration().Configure(modelBuilder.Entity<Localization>());
        new TagEntityConfiguartion().Configure(modelBuilder.Entity<Tag>());
        
        new PostEntityConfiguration().Configure(modelBuilder.Entity<Post>());
        new PostVideoEntityConfiguration().Configure(modelBuilder.Entity<PostVideo>());
        new PostCommentEntityConfiguration().Configure(modelBuilder.Entity<PostBaseComment>());
        new PostReactionEntityConfiguration().Configure(modelBuilder.Entity<PostReaction>());
    }
}