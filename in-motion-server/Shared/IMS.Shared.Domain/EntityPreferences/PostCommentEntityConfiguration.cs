using IMS.Shared.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Shared.Domain.EntityPreferences;

public class PostCommentEntityConfiguration: IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.ToTable("post_comment");

        builder.Property(pc => pc.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(pc => pc.Content)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(pc => pc.CreationDate)
            .HasColumnName("creation_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(pc => pc.LastModifiedDate)
            .HasColumnName("last_modified_date")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

    }
}