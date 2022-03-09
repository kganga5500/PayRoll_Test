using PaylocityProduct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaylocityProduct.Domain.Interfaces
{
  public  interface IDeductionCalculator
    {
        decimal CalculateDeductionPerPaycheck(Models.Employee employee);

        decimal CalculateDeductionPerAnnum(Employee employee);
    }
}
