using System;
using System.Collections.Generic;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Infrastructure.Context;
using PaylocityProduct.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;

namespace PaylocityProduct.Infrastructure.Tests
{
    public class EmployeeRepositoryTests
    {
        private readonly DbContextOptions<PayrollDBContext> _options;

        public EmployeeRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<PayrollDBContext>()
                 .UseInMemoryDatabase($"PayrollDatabase{Guid.NewGuid()}")
                 .Options;
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOfEmployee_WhenEmployeesExist()
        {
            await using (var context = new PayrollDBContext(_options))
            {
                CreateData(context);


                var employeeRepository = new EmployeeRepository(context);
                var employees = await employeeRepository.GetAll();

                Assert.NotNull(employees);
                Assert.IsType<List<Employee>>(employees);
            }
        }

        [Fact]
        public async void GetAll_ShouldReturnAnEmptyList_WhenEmployeesDoNotExist()
        {

            await using (var context = new PayrollDBContext(_options))
            {
                var employeeRepository = new EmployeeRepository(context);
                var employees = await employeeRepository.GetAll();

                Assert.NotNull(employees);
                Assert.Empty(employees);
                Assert.IsType<List<Employee>>(employees);
            }
        }

       

        [Fact]
        public async void GetById_ShouldReturnEmployeeWithSearchedId_WhenEmployeeWithSearchedIdExist()
        {

            await using (var context = new PayrollDBContext(_options))
            {
                CreateData(context);

                var employeeRepository = new EmployeeRepository(context);
                
                var employee = await employeeRepository.GetEmployeeById(2);

                Assert.NotNull(employee);
                Assert.IsType<Employee>(employee);
            }
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenEmployeeWithSearchedIdDoesNotExist()
        {

            await using (var context = new PayrollDBContext(_options))
            {
                var employeeRepository = new EmployeeRepository(context);
                var employee = await employeeRepository.GetEmployeeById(10);

                Assert.Null(employee);
            }
        }
        [Fact]
        public async void AddCategory_ShouldAddCategoryWithCorrectValues_WhenCategoryIsValid()
        {
            Employee employeeToAdd = new Employee();

            await using (var context = new PayrollDBContext(_options))
            {
                var employeeRepository = new EmployeeRepository (context);
                employeeToAdd = EmployeeList().First();

                await employeeRepository.AddEmployeeAsync(employeeToAdd);
            }

            await using (var context = new PayrollDBContext(_options))
            {
                var employeeResult = await context.Employees.Where(b => b.EmployeeId == 1).FirstOrDefaultAsync();

                Assert.NotNull(employeeResult);
                Assert.IsType<Employee>(employeeToAdd);
                Assert.Equal(employeeToAdd.EmployeeId, employeeResult.EmployeeId);
                Assert.Equal(employeeToAdd.FirstName, employeeResult.FirstName);
            }
        }








        private List<Employee> EmployeeList()
        {

            return new List<Employee>()
            {
              new Employee(false)
            {
                EmployeeId = 1,
                FirstName = "Employee First Test 1",
                LastName = "Employee Last Test 1",

                BenefitCostId = 1,
            },
                new Employee(false)
            {
                EmployeeId = 2,
                FirstName = "Employee First Test 2",
                LastName = "Employee Last Test 2",

                BenefitCostId = 1,
            }
        };

        }
        private static void CreateData(PayrollDBContext payrollDbContext)
        {
            payrollDbContext.BenefitCosts.Add(new BenefitCost { BenefitCostId = 1, Rate = 1000 });
            payrollDbContext.BenefitCosts.Add(new BenefitCost { BenefitCostId = 2, Rate = 500 });
            payrollDbContext.Employees.Add(new Employee(false)
            {
                EmployeeId = 1,
                FirstName = "Employee First Test 1",
                LastName = "Employee Last Test 1",

                BenefitCostId = 1,              
            });
            payrollDbContext.Employees.Add(new Employee(false)
            {
                EmployeeId = 2,
                FirstName = "Employee First Test 2",
                LastName = "Employee Last Test 2",

                BenefitCostId = 1,
            });


            payrollDbContext.Dependents.Add(
                        new Dependent(false)
                        {

                            EmployeeId = 2,
                            FirstName = "DEpendent First Test 3",
                            LastName = "DEpendent Last Test 3",

                           // BenefitCostId = 2
                        });
            payrollDbContext.Dependents.Add(new Dependent(false)
            {

                EmployeeId = 2,
                FirstName = "DEpendent First Test 4",
                LastName = "DEpendent Last Test 4",

              //  BenefitCostId = 2
            });




            payrollDbContext.Dependents.Add(new Dependent(false)
            {

                EmployeeId = 3,
                FirstName = "DEpendent First Test 5",
                LastName = "DEpendent Last Test 5",

                //BenefitCostId = 2
            });
                     payrollDbContext.Dependents.Add(new Dependent(false)
                     {

                         EmployeeId = 3,
                         FirstName = "DEpendent First Test 6",
                         LastName = "DEpendent Last Test 6",

                        // BenefitCostId = 2
                     });

                   

           
            payrollDbContext.SaveChangesAsync();
        }
    }
}