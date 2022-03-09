using PaylocityProduct.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaylocityProduct.Infrastructure.Mappings
{
    public static class BenefitCostMapping
    {
        public static void MapBenefitRates(this ModelBuilder builder)
        {
            builder.Entity<BenefitCost>(benefitCost =>
            {
                benefitCost.ToTable("BenefitCost");
                benefitCost.HasKey(x => x.BenefitCostId);
            });
        }
    }
}
