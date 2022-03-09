using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaylocityProduct.Domain.Services
{
    public class DeductionCalculator : IDeductionCalculator
    {


        public decimal CalculateDeductionPerPaycheck(Employee employee)
        {
            var perPaycheck= CalculateDeductionPerAnnum(employee) / Constants.NumberOfPaychecks;
          return  decimal.Round(perPaycheck, 2, MidpointRounding.AwayFromZero); 
        }
        public decimal CalculateDeductionPerAnnum(Employee employee)
        {
            decimal deduction = ApplyDiscount(employee.BenefitCost.Rate,employee.FirstName);
            if (employee.Dependents != null && employee.Dependents.Count > 0)
            {
                deduction = deduction + employee.Dependents.Sum(x => ApplyDiscount( x.BenefitCost.Rate,x.FirstName));
            }

            return decimal.Round(deduction, 2, MidpointRounding.AwayFromZero); 
        }

       public  decimal ApplyDiscount(decimal rate, string Name)
        {
            if (Name?.ToLower().StartsWith("a") ?? false)
                return rate *(1-Constants.DiscountPercentage); // 10 percent discount rate
            else
                return rate; // no discount
        }

    }
}
