using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(long id);
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<IEnumerable<Employee>> Search(string employeeName);
    }
}