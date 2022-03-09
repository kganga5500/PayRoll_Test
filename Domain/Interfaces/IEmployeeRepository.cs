using System.Collections.Generic;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeeById(long employeeId);
        Task AddEmployeeAsync(Employee employee);
    }
}