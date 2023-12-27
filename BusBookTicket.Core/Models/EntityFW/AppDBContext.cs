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
            modelBuilder.ApplyConfiguration<Account>(new AccountConfigs());
            modelBuilder.ApplyConfiguration<AdministrativeRegion>(new AdministrativeRegionConfigs());
            modelBuilder.ApplyConfiguration<AdministrativeUnit>(new AdministrativeUnitConfigs());
            modelBuilder.ApplyConfiguration<Bus>(new BusConfigs());
            modelBuilder.ApplyConfiguration<BusStation>(new BusStationConfigs());
            modelBuilder.ApplyConfiguration<BusType>(new BusTypeConfigs());
            modelBuilder.ApplyConfiguration<District>(new DistrictConfigs());
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfigs());
            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfigs());
            modelBuilder.ApplyConfiguration<Discount>(new DiscountConfigs());
            modelBuilder.ApplyConfiguration<Rank>(new RankConfigs());
            modelBuilder.ApplyConfiguration<Role>(new RoleConfigs());
            modelBuilder.ApplyConfiguration<Review>(new ReviewConfigs());
            modelBuilder.ApplyConfiguration<TicketItem>(new TicketItemConfigs());
            modelBuilder.ApplyConfiguration<Bill>(new BillConfigs());
            modelBuilder.ApplyConfiguration<BillItem>(new BillItemConfigs());
            modelBuilder.ApplyConfiguration<Ticket>(new TicketConfigs());
            modelBuilder.ApplyConfiguration<Seat>(new SeatConfigs());
            modelBuilder.ApplyConfiguration<SeatType>(new SeatTyeConfigs());
            modelBuilder.ApplyConfiguration<Province>(new ProvinceConfigs());
            modelBuilder.ApplyConfiguration<Ward>(new WardConfigs());
            modelBuilder.ApplyConfiguration<District>(new DistrictConfigs());
            modelBuilder.ApplyConfiguration<Images>(new ImgaesConfigs());
            modelBuilder.ApplyConfiguration<OtpCode>(new OtpCodeConfigs());
            modelBuilder.ApplyConfiguration<PriceClassification>(new PriceClassificationConfig());
            modelBuilder.ApplyConfiguration<Routes>(new RouteConfigs());
            modelBuilder.ApplyConfiguration<RouteDetail>(new RouteDetailConfigs());
            modelBuilder.ApplyConfiguration<StopStation>(new StopStationConfigs());
            modelBuilder.ApplyConfiguration<Prices>(new PricesConfigs());
            modelBuilder.ApplyConfiguration<Ticket_RouteDetail>(new Ticket_RouteDetailConfigs());
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusType> BusTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
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
        public DbSet<AdministrativeRegion> AdministrativeRegions { get; set; }
        public DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<PriceClassification> PriceClassifications { get; set; }
        public DbSet<Routes>Routes { get; set; }
        public DbSet<RouteDetail>RouteDetails { get; set; }
        public DbSet<StopStation>StopStations { get; set; }
        public DbSet<Ticket_RouteDetail> TicketRouteDetails { get; set; }
        public DbSet<Prices>Prices { get; set; }
        
}
}
