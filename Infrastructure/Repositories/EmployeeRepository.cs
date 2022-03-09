using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PaylocityProduct.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PayrollDBContext context) : base(context) { }

        public override async Task<List<Employee>> GetAll()
        {
            return await Db.Employees.Include(employee => employee.BenefitCost)

                .Include(employee => employee.Dependents)
                        .ThenInclude(dependent => dependent.BenefitCost)


                    .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(long employeeId)
        {
            var employess = await Db.Employees
                               .Where(employee => employee.EmployeeId == employeeId)
                              .Include(employee => employee.Dependents)
                                  .ThenInclude(dependent => dependent.BenefitCost)

                              .Include(employee => employee.BenefitCost).ToListAsync();

                   return employess.FirstOrDefault();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            employee.BenefitCost = Db.BenefitCosts.Find(employee.BenefitCostId);
            Db.Employees.Add(employee);
            await Db.SaveChangesAsync();
        }
    
    }
}