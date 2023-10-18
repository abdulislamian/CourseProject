using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Dtos.Teams;
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
    public class TeamUpdateValidator : AbstractValidator<TeamUpdate>
    {
        public TeamUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
