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

namespace PaylocityProduct.API.Controllers
{
    [Route("api/[controller]")]
    public class DependentController : ControllerBase
    {
        private readonly IDependentService _dependentService;
        private readonly IMapper _mapper;

        public DependentController(IMapper mapper,
                                IDependentService dependentService)
        {
            _mapper = mapper;
            _dependentService = dependentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var dependents = await _dependentService.GetAll();

            return Ok(_mapper.Map<IEnumerable<DependentResult>>(dependents));
        }


      

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(DependentAddDto dependentAddDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var dependent = _mapper.Map<Dependent>(dependentAddDto);
            dependent.BenefitCostId = 2;

            var dependentResult = await _dependentService.Add(dependent);

            if (dependentResult == null) return BadRequest();

            return Ok(dependentResult);
        }

       

    }
}