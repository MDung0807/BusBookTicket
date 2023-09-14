using BusBookTicket.Models.Entity;
using BusBookTicket.Models.EntityFW.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Models.EntityFW
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountConfigs());
        }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusType> BusesType { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
    }
}
