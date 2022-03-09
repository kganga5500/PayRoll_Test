using AutoMapper;
using PaylocityProduct.API.Dtos.Employee;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Employee, EmployeeAddDto>().ReverseMap();
            CreateMap<Employee, EmployeeResult>().ReverseMap();
            CreateMap<Dependent, DependentAddDto>().ReverseMap();

         
        }
    }
}