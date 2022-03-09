using System;
using System.Collections.Generic;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Domain.Services;
using Moq;
using Xunit;

namespace PaylocityProduct.Domain.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOfEmployee_WhenEmployeesExist()
        {
            var employees = CreateEmployeeList();

            _employeeRepositoryMock.Setup(c => c.GetAll()).ReturnsAsync(employees);

            var result = await _employeeService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenEmployeesDoNotExist()
        {
            _employeeRepositoryMock.Setup(c => c.GetAll())
                .ReturnsAsync((List<Employee>)null);

            var result = await _employeeService.GetAll();

            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _employeeRepositoryMock.Setup(c => c.GetAll())
                .ReturnsAsync(new List<Employee>());

            await _employeeService.GetAll();

            _employeeRepositoryMock.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnEmployee_WhenEmployeeExist()
        {
            var employee = CreateEmployee();

            _employeeRepositoryMock.Setup(c => c.GetEmployeeById(employee.EmployeeId))
                .ReturnsAsync(employee);

            var result = await _employeeService.GetById(employee.EmployeeId);

            Assert.NotNull(result);
            Assert.IsType<Employee>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            _employeeRepositoryMock.Setup(c => c.GetById(1))
                .ReturnsAsync((Employee)null);

            var result = await _employeeService.GetById(1);

            Assert.Null(result);
        }

        [Fact]
        public async void Add_ShouldAddEmployee_WhenEmployeeNameDoesNotExist()
        {
            var employee = CreateEmployee();

            _employeeRepositoryMock.Setup(c =>
                c.Search(c => c.EmployeeId == employee.EmployeeId))
                .ReturnsAsync(new List<Employee>());
            _employeeRepositoryMock.Setup(c => c.Add(employee));

            var result = await _employeeService.Add(employee);

            Assert.NotNull(result);
            Assert.IsType<Employee>(result);
        }

        private Employee CreateEmployee()
        {
            return new Employee()
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