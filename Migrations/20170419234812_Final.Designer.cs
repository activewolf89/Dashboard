using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dashboard.Models;

namespace Dashboard.Migrations
{
    [DbContext(typeof(DashboardContext))]
    [Migration("20170419234812_Final")]
    partial class Final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Dashboard.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentConversation");

                    b.Property<DateTime>("Created_At");

                    b.Property<int>("MessageId");

                    b.Property<int>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Dashboard.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created_At");

                    b.Property<int>("CreatorId");

                    b.Property<string>("MessageConversation");

                    b.Property<int>("MessageReceiverId");

                    b.HasKey("MessageId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MessageReceiverId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Dashboard.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("App_Level");

                    b.Property<DateTime>("Created_At");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Dashboard.Models.Comment", b =>
                {
                    b.HasOne("Dashboard.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Models.Message", b =>
                {
                    b.HasOne("Dashboard.Models.User", "Creator")
                        .WithMany("Messages")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.Models.User", "MessageReceiver")
                        .WithMany("Received_Messages")
                        .HasForeignKey("MessageReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
