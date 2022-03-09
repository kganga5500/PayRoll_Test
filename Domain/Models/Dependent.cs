using PaylocityProduct.Domain.Enums;

namespace PaylocityProduct.Domain.Models
{
    public  class Dependent : Entity
    {
        public Dependent(bool benifit)
        { 
        }
            public Dependent()
        {
            BenefitCost = new BenefitCost { BenefitCostId = (byte)BenifitModel.Dependent, Rate = Constants.DependentCost };
        }
        public long DependentId { get; set; }
        public long EmployeeId { get; set; }

    }
}