using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace PaylocityProduct.Infrastructure.Repositories
{
    public class DependentRepository : Repository<Dependent>, IDependentRepository
    {
        public DependentRepository(PayrollDBContext context) : base(context) { }

      
        async Task<IEnumerable<Dependent>> IDependentRepository.GetAll()
        {
            return await Db.Dependents.Include(b => b.BenefitCost)
                          .OrderBy(b => b.FirstName)
                          .ToListAsync();
        }
        public async Task Add(Dependent dependent)
        {
            dependent.BenefitCost = Db.BenefitCosts.Find(dependent.BenefitCostId);
            Db.Dependents.Add(dependent);
            await Db.SaveChangesAsync();
        }

    }
}