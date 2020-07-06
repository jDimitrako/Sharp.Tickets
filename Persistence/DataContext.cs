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

        public DbSet<Value> Values { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Value>()
                .HasData(
                    new Value { Id = 1, Name = "Value 101" },
                    new Value { Id = 2, Name = "Value 102" },
                    new Value { Id = 3, Name = "Value 103" }
                );

            builder.Entity<UserTicket>(x => x.HasKey(ua => new { ua.AppUserId, ua.TicketId }));

            builder.Entity<UserTicket>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.UserTickets)
                .HasForeignKey(u => u.AppUserId);

            builder.Entity<UserTicket>()
                .HasOne(a => a.Ticket)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(a => a.TicketId);
        }
    }
}