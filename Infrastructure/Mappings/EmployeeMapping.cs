using PaylocityProduct.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaylocityProduct.Infrastructure.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.EmployeeId);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar(150)");
            builder.Property(c => c.LastName)
              .IsRequired()
              .HasColumnType("varchar(150)");

            // 1 : N => Category : Employees
            builder.HasMany(c => c.Dependents)
                   .WithOne().HasForeignKey(dependent => dependent.EmployeeId);
            builder.HasOne(e => e.BenefitCost);

            builder.ToTable("Employees");
        }
    }
}