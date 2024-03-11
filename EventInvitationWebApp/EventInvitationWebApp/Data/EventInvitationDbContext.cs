using Duende.IdentityServer.EntityFramework.Options;
using EventInvitationWebApp.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventInvitationWebApp.Data
{
    public class EventInvitationDbContext : ApiAuthorizationDbContext<User>
    {
        public EventInvitationDbContext(DbContextOptions<EventInvitationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userID1 = Guid.NewGuid().ToString();
            var userID2 = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(
                   new User
                   {
                       UserName = "admin@event.com",
                       NormalizedUserName = "admin@event.com".ToUpper(),
                       Id = userID1,
                       Email = "admin@event.com",
                       NormalizedEmail = "admin@event.com".ToUpper(),
                       PasswordHash = hasher.HashPassword(null, "Admin@123")
                   },
                   new User
                   {
                       UserName = "user@event.com",
                       NormalizedUserName = "user@event.com".ToUpper(),
                       Id = userID2,
                       Email = "user@event.com",
                       NormalizedEmail = "user@event.com".ToUpper(),
                       PasswordHash = hasher.HashPassword(null, "User@123")
                   }
               );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER".ToUpper() }
            );

            //modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = userID1, RoleId = "1" },
                new IdentityUserRole<string> { UserId = userID2, RoleId = "2" }
            );


            // Define relationships, indexes, etc.
            modelBuilder.Entity<Invitation>()
                .HasKey(i => new { i.EventId, i.UserId });

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.EventId)
                .OnDelete(DeleteBehavior.Cascade); //DeleteBehavior."Cascade" means that if an Event is deleted, its related Invitations will also be deleted

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.InvitedUser)
                .WithMany(u => u.ReceivedInvitations)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict); //DeleteBehavior.Restrict means that if a User is deleted, it won't delete the associated Invitations
        }

    }
}
