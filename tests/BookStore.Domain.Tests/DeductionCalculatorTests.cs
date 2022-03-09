using System;
using System.Collections.Generic;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Domain.Services;
using Moq;
using Xunit;

namespace PaylocityProduct.Domain.Tests
{
    public class DeductionCalculatorTests
    {
        private readonly DeductionCalculator _deductionCalculator;

        public DeductionCalculatorTests()
        {
            _deductionCalculator = new DeductionCalculator();
        }

        [Fact]
        public void Verify_Valid_Discount_StartswithA()
        {
            var employee = CreateEmployee();
            decimal rate = 1000;
            var annualRatewithDiscount = _deductionCalculator.ApplyDiscount(rate, "Adam");
            var expectedAnnualRateWithDiscount = rate * (1 - Constants.DiscountPercentage);
            Assert.Equal(expectedAnnualRateWithDiscount, annualRatewithDiscount);
        }
        [Fact]
        public void Verify_Valid_Discount_Startswitha()
        {
            var employee = CreateEmployee();
            decimal rate = 1000;
            var annualRatewithDiscount = _deductionCalculator.ApplyDiscount(rate, "adam");
            var expectedAnnualRateWithDiscount = rate * (1 - Constants.DiscountPercentage);
            Assert.Equal(expectedAnnualRateWithDiscount, annualRatewithDiscount);
        }
        [Fact]
        public void Verify_Deduction_WithDependents_Employee_Starts_Not_withA()
        {
            var employee = CreateEmployeeNotStartsWithA();
            var annualRatewithDiscount = _deductionCalculator.CalculateDeductionPerAnnum(employee);
            var expectedAnnualRateWithDiscount = 1000  + 500 + 500;
            Assert.Equal(expectedAnnualRateWithDiscount, annualRatewithDiscount);
        }
        [Fact]
        public void Verify_Deduction_WithDependents_Employee_StartswithA()
        {
            var employee = CreateEmployee();
            var annualRatewithDiscount = _deductionCalculator.CalculateDeductionPerAnnum(employee);
            var expectedAnnualRateWithDiscount = 1000 * (1 - Constants.DiscountPercentage)+500+500;
            Assert.Equal(expectedAnnualRateWithDiscount, annualRatewithDiscount);
        }
        [Fact]
        public void Verify_Deduction_WithDependents_Employee_StartswithA_Dependent_Starts_WithA()
        {
            var employee = CreateEmployeeWithDependentDiscount();
            var annualRatewithDiscount = _deductionCalculator.CalculateDeductionPerAnnum(employee);
            var expectedAnnualRateWithDiscount = 1000 * (1 - Constants.DiscountPercentage) + 500 * (1 - Constants.DiscountPercentage) + 500;
            Assert.Equal(expectedAnnualRateWithDiscount, annualRatewithDiscount);
        }
     
        [Fact]
        public void Verify_Deduction_PerPaycheck_WithDependents_Employee_StartswithA()
        {
            var employee = CreateEmployee();
            var paycheckRate = _deductionCalculator.CalculateDeductionPerPaycheck(employee);
            var expectedpaycheckRate = 1000 * (1 - Constants.DiscountPercentage)+500+500;
            expectedpaycheckRate =  decimal.Round(expectedpaycheckRate / Constants.NumberOfPaychecks, 2, MidpointRounding.AwayFromZero);
            Assert.Equal(expectedpaycheckRate, paycheckRate);
        }
        [Fact]
        public void Verify_Deduction_PerPaycheck_WithDependents_Employee_StartswithA_Dependent_Starts_WithA()
        {
            var employee = CreateEmployeeWithDependentDiscount();
            var paycheckRate = _deductionCalculator.CalculateDeductionPerPaycheck(employee);
            var expectedpaycheckRate = 1000 * (1 - Constants.DiscountPercentage) + 500 * (1 - Constants.DiscountPercentage) + 500;
            expectedpaycheckRate = decimal.Round(expectedpaycheckRate / Constants.NumberOfPaychecks, 2, MidpointRounding.AwayFromZero);

            Assert.Equal(expectedpaycheckRate, paycheckRate);
        }


        private Employee CreateEmployee()
        {
            return new Employee()
            {
                EmployeeId = 1,
                FirstName = "AEmployee First Test 1",
                LastName = "Employee Last Test 1",

                BenefitCostId = 1,
                BenefitCost= new BenefitCost() { BenefitCostId=1,Rate =1000},
                Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 1",
                    LastName = "DEpendent Last Test 1",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 2",
                    LastName = "DEpendent Last Test 2",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        }

                    }
            };
        }
        private Employee CreateEmployeeNotStartsWithA()
        {
            return new Employee()
            {
                EmployeeId = 1,
                FirstName = "Employee First Test 1",
                LastName = "Employee Last Test 1",

                BenefitCostId = 1,
                BenefitCost = new BenefitCost() { BenefitCostId = 1, Rate = 1000 },
                Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 1",
                    LastName = "DEpendent Last Test 1",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 2",
                    LastName = "DEpendent Last Test 2",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        }

                    }
            };
        }
        private Employee CreateEmployeeWithDependentDiscount()
        {
            return new Employee()
            {
                EmployeeId = 1,
                FirstName = "AEmployee First Test 1",
                LastName = "Employee Last Test 1",

                BenefitCostId = 1,
                BenefitCost = new BenefitCost() { BenefitCostId = 1, Rate = 1000 },
                Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "ADEpendent First Test 1",
                    LastName = "DEpendent Last Test 1",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 2",
                    LastName = "DEpendent Last Test 2",
                    BenefitCost= new BenefitCost() { BenefitCostId=2,Rate =500},
                    BenefitCostId = 2
                        }

                    }
            };
        }
        private List<Employee> CreateEmployeeList()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    EmployeeId = 1,
                    FirstName = "Employee First Test 1",
                    LastName = "Employee Last Test 1",

                    BenefitCostId = 1,
                    Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 1",
                    LastName = "DEpendent Last Test 1",

                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 1,
                    FirstName = "DEpendent First Test 2",
                    LastName = "DEpendent Last Test 2",

                    BenefitCostId = 2
                        }

                    }
                },

                new Employee()
                {
                    EmployeeId = 2,
                    FirstName = "Employee First Test 2",
                    LastName = "Employee Last Test 2",

                    BenefitCostId = 1,
                    Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 2,
                    FirstName = "DEpendent First Test 3",
                    LastName = "DEpendent Last Test 3",

                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 2,
                    FirstName = "DEpendent First Test 4",
                    LastName = "DEpendent Last Test 4",

                    BenefitCostId = 2
                        }

                    }
                },

                new Employee()
                {
                    EmployeeId = 3,
                    FirstName = "Employee First Test 3",
                    LastName = "Employee Last Test 3",

                    BenefitCostId = 1,
                    Dependents = new List<Dependent>()
                    {
                        new Dependent()
                        {

                    EmployeeId = 3,
                    FirstName = "DEpendent First Test 5",
                    LastName = "DEpendent Last Test 5",

                    BenefitCostId = 2
                        },
                         new Dependent()
                        {

                    EmployeeId = 3,
                    FirstName = "DEpendent First Test 6",
                    LastName = "DEpendent Last Test 6",

                    BenefitCostId = 2
                        }

                    }
                },
            };
        }
    }
}