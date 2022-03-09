using System;
using System.ComponentModel.DataAnnotations;

namespace PaylocityProduct.API.Dtos.Employee
{
    public class EmployeeResult
    {
       
        public long EmployeeId { get; set; }

  
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int DependentCount { get; set; }
        public decimal AnnualDeductionCost { get; set; }
        public decimal DeductionCostPerPaycheck { get; set; }


    }
}