using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Dtos.Job;
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
    public class JobCreateValidator : AbstractValidator<JobCreate>
    {
        public JobCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
        }
    }
}
