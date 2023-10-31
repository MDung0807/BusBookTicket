using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Core.Models.EntityFW
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

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
            modelBuilder.ApplyConfiguration(new RoleConfigs());
            modelBuilder.ApplyConfiguration(new ReviewConfigs());
            modelBuilder.ApplyConfiguration(new TicketItemConfigs());
            modelBuilder.ApplyConfiguration(new BillConfigs());
            modelBuilder.ApplyConfiguration(new BillItemConfigs());
            modelBuilder.ApplyConfiguration(new TicketConfigs());
            modelBuilder.ApplyConfiguration(new SeatConfigs());
            modelBuilder.ApplyConfiguration(new SeatTyeConfigs());
            modelBuilder.ApplyConfiguration(new ImgaesConfigs());

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
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Images> Images { get; set; }
}
}
