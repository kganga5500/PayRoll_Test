using System;
using System.Collections.Generic;
using System.Text;

namespace PaylocityProduct.Domain.Models
{
    public abstract class Entity
    {
        // public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BenefitCost BenefitCost { get; set; }
        public byte BenefitCostId { get; set; }

    }

}
