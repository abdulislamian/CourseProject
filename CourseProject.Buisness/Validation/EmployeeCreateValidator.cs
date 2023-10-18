using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Model;
using CourseProject.Common.Dtos.Address;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Buisness.Validation
{
    public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
    {
        public EmployeeCreateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        }
    }
}
