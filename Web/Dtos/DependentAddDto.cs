using System;
using System.ComponentModel.DataAnnotations;

namespace PaylocityProduct.API.Dtos.Employee
{
    public class DependentAddDto
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public long EmployeeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string LastName { get; set; }

    }
}