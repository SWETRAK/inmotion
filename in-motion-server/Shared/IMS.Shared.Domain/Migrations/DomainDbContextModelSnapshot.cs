﻿// <auto-generated />
using System;
using IMS.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS.Shared.Domain.Migrations
{
    [DbContext(typeof(DomainDbContext))]
    partial class DomainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Friendship.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid>("FirstUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("first_user_id");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification_date");

                    b.Property<Guid>("SecondUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("second_user_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("FirstUserId");

                    b.HasIndex("Id");

                    b.HasIndex("SecondUserId");

                    b.ToTable("friendships", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Other.Localization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("localizations", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Other.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("description");

                    b.Property<Guid>("FrontVideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("front_video_id");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_date");

                    b.Property<Guid>("LocalizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("localization_id");

                    b.Property<Guid>("RearVideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("rear_video_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FrontVideoId")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.HasIndex("LocalizationId");

                    b.HasIndex("RearVideoId")
                        .IsUnique();

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_date");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.HasIndex("PostId");

                    b.ToTable("post_comments", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostCommentReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Emoji")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("emoji");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification_date");

                    b.Property<Guid>("PostCommentId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_comment_id");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.HasIndex("PostCommentId");

                    b.ToTable("post_comment_reaction", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Emoji")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("emoji");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modification_date");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.HasIndex("PostId");

                    b.ToTable("post_reactions", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostVideo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthrorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("BucketLocation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_location");

                    b.Property<string>("BucketName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_name");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.Property<DateTime>("LastEditionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_edition_name");

                    b.Property<Guid>("PostFrontId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_front_id");

                    b.Property<Guid>("PostRearId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_rear_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id");

                    b.ToTable("post_videos", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auth_key");

                    b.Property<int>("Name")
                        .HasColumnType("integer")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("providers", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("bio");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("nickname");

                    b.Property<Guid>("ProfileVideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("profile_video_id");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProfileVideoId")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.UserProfileVideo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthrorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("BucketLocation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_location");

                    b.Property<string>("BucketName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bucket_name");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.Property<DateTime>("LastEditionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_edition_name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("user_profile_videos", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("PostId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("posts_tags_relations", (string)null);
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Friendship.Friendship", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "FirstUser")
                        .WithMany()
                        .HasForeignKey("FirstUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "SecondUser")
                        .WithMany()
                        .HasForeignKey("SecondUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstUser");

                    b.Navigation("SecondUser");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Other.Tag", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.Post", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Post.PostVideo", "FrontVideo")
                        .WithOne("PostFront")
                        .HasForeignKey("IMS.Shared.Domain.Entities.Post.Post", "FrontVideoId")
                        .HasPrincipalKey("IMS.Shared.Domain.Entities.Post.PostVideo", "PostFrontId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Other.Localization", "Localization")
                        .WithMany()
                        .HasForeignKey("LocalizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Post.PostVideo", "RearVideo")
                        .WithOne("PostRear")
                        .HasForeignKey("IMS.Shared.Domain.Entities.Post.Post", "RearVideoId")
                        .HasPrincipalKey("IMS.Shared.Domain.Entities.Post.PostVideo", "PostRearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("FrontVideo");

                    b.Navigation("Localization");

                    b.Navigation("RearVideo");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostComment", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Post.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostCommentReaction", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Post.PostComment", "PostComment")
                        .WithMany()
                        .HasForeignKey("PostCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("PostComment");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostReaction", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Post.Post", "Post")
                        .WithMany("PostReactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostVideo", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.Provider", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.User", "User")
                        .WithMany("Providers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.User", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.User.UserProfileVideo", "ProfileVideo")
                        .WithOne("Author")
                        .HasForeignKey("IMS.Shared.Domain.Entities.User.User", "ProfileVideoId")
                        .HasPrincipalKey("IMS.Shared.Domain.Entities.User.UserProfileVideo", "AuthrorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileVideo");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("IMS.Shared.Domain.Entities.Post.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.Shared.Domain.Entities.Other.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.Post", b =>
                {
                    b.Navigation("PostComments");

                    b.Navigation("PostReactions");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.Post.PostVideo", b =>
                {
                    b.Navigation("PostFront");

                    b.Navigation("PostRear");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.User", b =>
                {
                    b.Navigation("Providers");
                });

            modelBuilder.Entity("IMS.Shared.Domain.Entities.User.UserProfileVideo", b =>
                {
                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
