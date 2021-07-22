using BankManagement.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BankManagement.WebAPI.Helpers
{
    public class DataContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("BankManagementWebApiDatabase"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasKey(s => s.CurrencyId);
            modelBuilder.Entity<Customer>().HasKey(s => s.CustomerId);
            modelBuilder.Entity<Deal>().HasKey(s => s.DealId);
            modelBuilder.Entity<ExchangeRate>().HasKey(s => s.ExchangeRateId);
            modelBuilder.Entity<Role>().HasKey(s => s.RoleId);
            modelBuilder.Entity<Service>().HasKey(s => s.ServiceId);

            
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }




    }
}
