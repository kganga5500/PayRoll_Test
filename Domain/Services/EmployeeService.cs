using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<Employee> GetById(long id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<Employee> Add(Employee employee)
        {
         

            await _employeeRepository.AddEmployeeAsync(employee);
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {

            await _employeeRepository.Update(employee);
            return employee;
        }


        public async Task<IEnumerable<Employee>> Search(string employeeName)
        {
            return await _employeeRepository.Search(c => c.FirstName.Contains(employeeName));
        }

        public void Dispose()
        {
            _employeeRepository?.Dispose();
        }
    }
}