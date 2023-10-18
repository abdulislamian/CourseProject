using AutoMapper;
using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using CourseProject.Buisness.Exceptions;
using CourseProject.Buisness.Validation;
using CourseProject.Common.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace Courseproject.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeCreateValidator createValidator;
    private readonly EmployeeUpdateValidator updateValidator;

    private IGenericRepository<Employee> EmployeeRepository { get; }
    public IGenericRepository<Job> JobRepository { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    private IMapper Mapper { get; }

    public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Job> jobRepository,
        IGenericRepository<Address> addressRepository, IMapper mapper,EmployeeCreateValidator _createValidator, EmployeeUpdateValidator _updateValidator)
    {
        EmployeeRepository = employeeRepository;
        JobRepository = jobRepository;
        AddressRepository = addressRepository;
        Mapper = mapper;
        createValidator = _createValidator;
        updateValidator = _updateValidator;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await createValidator.ValidateAndThrowAsync(employeeCreate);

        var address = await AddressRepository.GetByIdAsync(employeeCreate.AddressId);
        if (address == null)
            throw new AddressNotFoundException(employeeCreate.AddressId);

        var job = await JobRepository.GetByIdAsync(employeeCreate.JobId);
        if(job == null)
            throw new JobNotFoundException(employeeCreate.JobId);

        var entity = Mapper.Map<Employee>(employeeCreate);
        entity.Address = address;
        entity.Job = job;
        await EmployeeRepository.InsertAsync(entity);
        await EmployeeRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await EmployeeRepository.GetByIdAsync(employeeDelete.id);

        if (entity == null)
            throw new EmployeeNotFoundException(employeeDelete.id);

        EmployeeRepository.Delete(entity);
        await EmployeeRepository.SaveChangesAsync();

    }

    public async Task<EmployeeDetails> GetEmployeeAsync(int id)
    {
        var entity = await EmployeeRepository.GetByIdAsync(id,(employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);
        if(entity == null)
            throw new EmployeeNotFoundException(id);

        return Mapper.Map<EmployeeDetails>(entity);
    }

    public async Task<List<EmployeeList>> GetEmployeesAsnyc(EmployeeFilter employeeFilter)
    {
        //Not Clear (Expression)
        Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true :
        employee.FirstName.StartsWith(employeeFilter.FirstName);
        Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true :
        employee.LastName.StartsWith(employeeFilter.LastName);
        Expression<Func<Employee, bool>> jobfilter = (employee) => employeeFilter.Job == null ? true :
        employee.Job.Name.StartsWith(employeeFilter.Job);
        Expression<Func<Employee, bool>> teamFilter = (employee) => employeeFilter.Team == null ? true :
        employee.Teams.Any(team => team.Name.StartsWith(employeeFilter.Team));

        var entities = await EmployeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
        {
            firstNameFilter, lastNameFilter, jobfilter, teamFilter
        }, employeeFilter.Skip, employeeFilter.Take,
        (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);

        return Mapper.Map<List<EmployeeList>>(entities);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        await updateValidator.ValidateAndThrowAsync(employeeUpdate);

        var address = await AddressRepository.GetByIdAsync(employeeUpdate.AddressId);
        if(address == null)
            throw new AddressNotFoundException(employeeUpdate.AddressId);

        var job = await JobRepository.GetByIdAsync(employeeUpdate.JobId);
        if(job == null)
            throw new JobNotFoundException(employeeUpdate.JobId);

        var existingEmployee = await EmployeeRepository.GetByIdAsync(employeeUpdate.Id);
        if(existingEmployee ==null)
            throw new  EmployeeNotFoundException(employeeUpdate.Id);

        var entity = Mapper.Map<Employee>(employeeUpdate);
        entity.Address = address;
        entity.Job = job;
        EmployeeRepository.Update(entity);
        await EmployeeRepository.SaveChangesAsync();
    }
}
