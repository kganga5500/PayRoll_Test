using PaylocityProduct.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace PaylocityCodingChallenge.Data.Context.Mappers
{
    public static class DependentMapping
    {
        public static void MapDependents(this ModelBuilder builder)
        {
            builder.Entity<Dependent>(dependent =>
            {
                dependent.ToTable("Dependent");
                dependent.HasKey(x => x.DependentId);
                dependent.HasOne(x => x.BenefitCost);
            });
        }
    }
}
