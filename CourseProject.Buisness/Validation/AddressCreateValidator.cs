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
    public class AddressCreateValidator : AbstractValidator<AddressCreate>
    {
        public AddressCreateValidator()
        {
            RuleFor(address => address.Email).NotEmpty().EmailAddress();
            RuleFor(address => address.City).NotEmpty().MaximumLength(100);
            RuleFor(address => address.Street).NotEmpty().MaximumLength(100);
            RuleFor(address => address.Zip).NotEmpty().MaximumLength(16);
            RuleFor(address => address.Phone).MaximumLength(16);

        }
    }
}
