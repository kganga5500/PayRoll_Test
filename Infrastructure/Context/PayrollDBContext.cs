using System.Linq;
using PaylocityProduct.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace PaylocityProduct.Infrastructure.Context
{
    public class PayrollDBContext : DbContext
    {
        public PayrollDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<BenefitCost> BenefitCosts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PayrollDBContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BenefitCost>()
      .Property(p => p.Rate)
      .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BenefitCost>().HasData(
       new BenefitCost
       {
           BenefitCostId = 1,
           Rate = 1000
       }, new BenefitCost
       {
           BenefitCostId = 2,
           Rate = 500
       }
   );
        }
    }
}