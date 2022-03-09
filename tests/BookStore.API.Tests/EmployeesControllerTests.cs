using System;
using System.Collections.Generic;
using AutoMapper;
using PaylocityProduct.API.Controllers;
using PaylocityProduct.API.Dtos.Employee;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using PaylocityProduct.Domain.Services;
using System.Linq;

namespace EmployeeStore.API.Tests
{
    public class EmployeesControllerTests
    {
        private readonly EmployeeController _employeesController;
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly Mock<IDeductionCalculator> _deductionCalculatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public EmployeesControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _deductionCalculatorMock = new Mock<IDeductionCalculator>();
            _mapperMock = new Mock<IMapper>();
            _employeesController = new EmployeeController(_mapperMock.Object, _employeeServiceMock.Object, _deductionCalculatorMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenExistEmployees()
        {
            var _employeesController = new EmployeeController(_mapperMock.Object, _employeeServiceMock.Object, new DeductionCalculator());

            var employees = CreateEmployeeList();
            var dtoExpected = MapModelToEmployeeResultListDto(employees);
            _employeeServiceMock.Setup(c => c.GetAll()).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>())).Returns(dtoExpected.First());


            var result = await _employeesController.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenDoesNotExistAnyEmployee()
        {
            var _employeesController = new EmployeeController(_mapperMock.Object, _employeeServiceMock.Object, new DeductionCalculator());

            var employees = new List<Employee>();
            var dtoExpected = MapModelToEmployeeResultListDto(employees);

            _employeeServiceMock.Setup(c => c.GetAll()).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeResult>>(It.IsAny<List<Employee>>())).Returns(dtoExpected);

            var result = await _employeesController.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromService_OnlyOnce()
        {

            var employees = CreateEmployeeList();
            var dtoExpected = MapModelToEmployeeResultListDto(employees);
            _deductionCalculatorMock.Setup(c => c.CalculateDeductionPerAnnum(CreateEmployee())).Returns(500);

            _employeeServiceMock.Setup(c => c.GetAll()).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>())).Returns(dtoExpected.First());

           // _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeResult>>(It.IsAny<List<Employee>>())).Returns(dtoExpected);

            await _employeesController.Get();

            _employeeServiceMock.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnOk_WhenEmployeeExist()
        {
            var employee = CreateEmployee();
            var dtoExpected = MapModelToEmployeeResult(employee);
            _deductionCalculatorMock.Setup(c => c.CalculateDeductionPerAnnum(CreateEmployee())).Returns(500);
            _employeeServiceMock.Setup(c => c.GetById(2)).ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>())).Returns(dtoExpected);

            var result = await _employeesController.GetById(2);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
        {
            _employeeServiceMock.Setup(c => c.GetById(2)).ReturnsAsync((Employee)null);

            var result = await _employeesController.GetById(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetById_ShouldCallGetByIdFromService_OnlyOnce()
        {
            var employee = CreateEmployee();
            var dtoExpected = MapModelToEmployeeResult(employee);

            _employeeServiceMock.Setup(c => c.GetById(2)).ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>())).Returns(dtoExpected);

            await _employeesController.GetById(2);

            _employeeServiceMock.Verify(mock => mock.GetById(2), Times.Once);
        }

        [Fact]
        public async void Add_ShouldReturnOk_WhenEmployeeIsAdded()
        {
            var employee = CreateEmployee();
            var employeeAddDto = new EmployeeAddDto() { FirstName = employee.FirstName };
            var employeeResultDto = MapModelToEmployeeResult(employee);

            _mapperMock.Setup(m => m.Map<Employee>(It.IsAny<EmployeeAddDto>())).Returns(employee);
            _employeeServiceMock.Setup(c => c.Add(employee)).ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>())).Returns(employeeResultDto);

            var result = await _employeesController.Add(employeeAddDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var employeeAddDto = new EmployeeAddDto();
            _employeesController.ModelState.AddModelError("Name", "The field name is required");

            var result = await _employeesController.Add(employeeAddDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenEmployeeResultIsNull()
        {
            var employee = CreateEmployee();
            var employeeAddDto = new EmployeeAddDto() { FirstName = employee.FirstName };

            _mapperMock.Setup(m => m.Map<Employee>(It.IsAny<EmployeeAddDto>())).Returns(employee);
            _employeeServiceMock.Setup(c => c.Add(employee)).ReturnsAsync((Employee)null);

            var result = await _employeesController.Add(employeeAddDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Add_ShouldCallAddFromService_OnlyOnce()
        {
            var employee = CreateEmployee();
            var employeeAddDto = new EmployeeAddDto() { FirstName = employee.FirstName };

            _mapperMock.Setup(m => m.Map<Employee>(It.IsAny<EmployeeAddDto>())).Returns(employee);
            _employeeServiceMock.Setup(c => c.Add(employee)).ReturnsAsync(employee);

            await _employeesController.Add(employeeAddDto);

            _employeeServiceMock.Verify(mock => mock.Add(employee), Times.Once);
        }

       

        [Fact]
        public async void Search_ShouldReturnNotFound_WhenEmployeeWithSearchedNameDoesNotExist()
        {
            var employee = CreateEmployee();
            var employeeList = new List<Employee>();

            var dtoExpected = MapModelToEmployeeResult(employee);
            employee.FirstName = dtoExpected.FirstName;

            _employeeServiceMock.Setup(c => c.Search(employee.FirstName)).ReturnsAsync(employeeList);
            _mapperMock.Setup(m => m.Map<IEnumerable<Employee>>(It.IsAny<Employee>())).Returns(employeeList);

            var result = await _employeesController.Search(employee.FirstName);
            var actual = (NotFoundObjectResult)result.Result;

            Assert.NotNull(actual);
            Assert.IsType<NotFoundObjectResult>(actual);
        }

        [Fact]
        public async void Search_ShouldCallSearchFromService_OnlyOnce()
        {
            var employeeList = CreateEmployeeList();
            var employee = CreateEmployee();

            _employeeServiceMock.Setup(c => c.Search(employee.FirstName)).ReturnsAsync(employeeList);
            _mapperMock.Setup(m => m.Map<List<Employee>>(It.IsAny<IEnumerable<Employee>>())).Returns(employeeList);

            await _employeesController.Search(employee.FirstName);

            _employeeServiceMock.Verify(mock => mock.Search(employee.FirstName), Times.Once);
        }

      
     

        private Employee CreateEmployee()
        {
          return  new Employee()
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

        private EmployeeResult MapModelToEmployeeResult(Employee employee)
        {
            var employeeDto = new EmployeeResult()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName

            };
            return employeeDto;
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

        private List<EmployeeResult> MapModelToEmployeeResultListDto(List<Employee> employees)
        {
            var listEmployees = new List<EmployeeResult>();

            foreach (var item in employees)
            {
                var employee = new EmployeeResult()
                {
                    EmployeeId = item.EmployeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                };
                listEmployees.Add(employee);
            }
            return listEmployees;
        }
    }
}