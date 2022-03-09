using PaylocityProduct.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaylocityProduct.Domain.Models
{
    public  class Employee : Entity
    {
        public Employee(bool benifit)
        {

        }
        public Employee()
        {
            BenefitCost = new BenefitCost { BenefitCostId = (byte)BenifitModel.Employee,Rate= Constants.EmployeeCost};

        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long EmployeeId { get; set; }

        public ICollection<Dependent> Dependents { get; set; }
    }
}