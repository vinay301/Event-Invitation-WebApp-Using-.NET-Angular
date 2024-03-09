﻿// <auto-generated />
using System;
using EventInvitationWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventInvitationWebApp.Migrations
{
    [DbContext(typeof(EventInvitationDbContext))]
    partial class EventInvitationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventInvitationWebApp.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.Invitation", b =>
                {
                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Response")
                        .HasColumnType("int");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "fa1766dd-dc24-4bb3-8f6d-d1319b4f6761",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "15068c3c-30e2-4d31-a79e-67bf5c58e26e",
                            Email = "admin@event.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EVENT.COM",
                            NormalizedUserName = "ADMIN@EVENT.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEH95qcPUbRcEwm6fh2K+XtQZ8IDJNrjyDmXzb57mkt06vlDOMuIT0Bh2XCk7+DewVQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8c65d276-b177-4f66-9ebf-299b0b11c10b",
                            TwoFactorEnabled = false,
                            UserName = "admin@event.com"
                        },
                        new
                        {
                            Id = "3fba370a-0a16-4a1b-aab0-d6a2062d8ffe",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "03c14729-7d62-4bf0-ae1c-e67512a2c025",
                            Email = "user@event.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@EVENT.COM",
                            NormalizedUserName = "USER@EVENT.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKkHYDvwIalBIYgnscunp0uiWYi7hkTqKCziQLWF1sJO4uzKriEsgJ0p87Z/9iQWJw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e8aab33e-985c-4a77-ac3e-749a40f98f7b",
                            TwoFactorEnabled = false,
                            UserName = "user@event.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("IdentityUserRole<string>");

                    b.HasData(
                        new
                        {
                            UserId = "fa1766dd-dc24-4bb3-8f6d-d1319b4f6761",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "3fba370a-0a16-4a1b-aab0-d6a2062d8ffe",
                            RoleId = "2"
                        });
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.Event", b =>
                {
                    b.HasOne("EventInvitationWebApp.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.Invitation", b =>
                {
                    b.HasOne("EventInvitationWebApp.Models.Event", "Event")
                        .WithMany("Invitations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventInvitationWebApp.Models.User", "InvitedUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("InvitedUser");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.User", b =>
                {
                    b.HasOne("EventInvitationWebApp.Models.Event", null)
                        .WithMany("InvitedUsers")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.Event", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("InvitedUsers");
                });

            modelBuilder.Entity("EventInvitationWebApp.Models.User", b =>
                {
                    b.Navigation("ReceivedInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
