using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Dtos.Teams;
using CourseProject.Common.Dtos.Address;

namespace Courseproject.Common.Dtos.Employee;

//Todo : Team
public record EmployeeDetails(int Id, string FirstName, string LastName, AddressGet Address, JobGet Job, List<TeamGet> Teams);