using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Common.Models.EntityFW
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountConfigs());
            modelBuilder.ApplyConfiguration(new BusConfigs());
            modelBuilder.ApplyConfiguration(new BusStationConfigs());
            modelBuilder.ApplyConfiguration(new BusStopConfigs());
            modelBuilder.ApplyConfiguration(new BusTypeConfigs());
            modelBuilder.ApplyConfiguration(new CompanyConfigs());
            modelBuilder.ApplyConfiguration(new CustomerConfigs());
            modelBuilder.ApplyConfiguration(new DiscountConfigs());
            modelBuilder.ApplyConfiguration(new RankConfigs());
            modelBuilder.ApplyConfiguration(new ReviewConfigs());
            modelBuilder.ApplyConfiguration(new SeatItemConfigs());
            modelBuilder.ApplyConfiguration(new TicketConfigs());
            modelBuilder.ApplyConfiguration(new TicketItemConfigs());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusType> BusesType { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<SeatItem> SeatItems { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
    }
}
