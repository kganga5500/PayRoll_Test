using System;
using System.Collections.Generic;
using System.Text;

namespace PaylocityProduct.Domain.Models
{
    public abstract class Person
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BenefitCost BenefitCost { get; set; }
    }
}
