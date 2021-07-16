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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("BankManagementWebApiDatabase"));
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }




    }
}
