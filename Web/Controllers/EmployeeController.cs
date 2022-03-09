using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaylocityProduct.API.Dtos.Employee;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaylocityProduct.API.Dtos;
using PaylocityProduct.Domain.Services;
using System;

namespace PaylocityProduct.API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDeductionCalculator _deductionCalculator;

        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper,
                                IEmployeeService employeeService,IDeductionCalculator deductionCalculator)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _deductionCalculator = deductionCalculator;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAll();
            var results = new List<EmployeeResult>();
            foreach (var employee in employees)
            {               
                results.Add(GetEmployeeResult(employee));     
            }
            return Ok(results);
        }       
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null) return NotFound();
        
            
            return Ok(GetEmployeeResult(employee));
        }

      

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(EmployeeAddDto employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest();
           
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.BenefitCostId = 1;
                var employeeResult = await _employeeService.Add(employee);

                if (employeeResult == null) return BadRequest();

                return Ok(employeeResult);
           
           
        
        }

       

        [HttpGet]
        [Route("search/{employeeName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Employee>>> Search(string employeeName)
        {
            var employees = _mapper.Map<List<Employee>>(await _employeeService.Search(employeeName));

            if (employees == null || employees.Count == 0) return NotFound("None employee was founded");

            return Ok(employees);
        }
        private EmployeeResult GetEmployeeResult(Employee employee)
        {
            var result = _mapper.Map<EmployeeResult>(employee);
            result.AnnualDeductionCost = _deductionCalculator.CalculateDeductionPerAnnum(employee);
            result.DeductionCostPerPaycheck = _deductionCalculator.CalculateDeductionPerPaycheck(employee);
            result.DependentCount = employee.Dependents != null ? employee.Dependents.Count : 0;
            return result;

        }
    }
}