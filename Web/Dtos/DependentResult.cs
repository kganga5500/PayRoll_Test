using System;
using System.ComponentModel.DataAnnotations;

namespace PaylocityProduct.API.Dtos.Employee
{
    public class DependentResult
    {
       
        public long EmployeeId { get; set; }

        public long DependentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}