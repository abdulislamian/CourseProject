using AutoMapper;
using Courseproject.Common.Dtos.Teams;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using CourseProject.Buisness.Exceptions;
using CourseProject.Buisness.Validation;
using CourseProject.Common.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace Courseproject.Business.Services;

public class TeamService : ITeamService
{
    private readonly TeamCreateValidator createValidator;
    private readonly TeamUpdateValidator updateValidator;

    private IGenericRepository<Team> TeamRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IMapper Mapper { get; }

    public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository,
        IMapper mapper,TeamCreateValidator _createValidator, TeamUpdateValidator _updateValidator)
    {
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        Mapper = mapper;
        createValidator = _createValidator;
        updateValidator = _updateValidator;
    }


    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        await createValidator.ValidateAndThrowAsync(teamCreate);

        //var abc = teamCreate.Employees.Contains(employee.Id);
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);

        var missingEmployee = teamCreate.Employees.Where((id) => !employees.Any(existing=> existing.Id == id));
        if (missingEmployee.Any())
            throw new EmployeesNotFoundException(missingEmployee.ToArray());

        var entity = Mapper.Map<Team>(teamCreate);
        entity.Employees = employees;
        await TeamRepository.InsertAsync(entity);
        await TeamRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var entity = await TeamRepository.GetByIdAsync(teamDelete.Id);
        if (entity == null)
            throw new TeamNotFoundException(entity.Id);

        TeamRepository.Delete(entity);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task<TeamGet> GetTeamAsync(int id)
    {
        var entity = await TeamRepository.GetByIdAsync(id, (team) => team.Employees); 
        if (entity == null)
            throw new TeamNotFoundException(id);

        return Mapper.Map<TeamGet>(entity);
    }

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var entities = await TeamRepository.GetAsync(null, null, (team) => team.Employees);
        return Mapper.Map<List<TeamGet>>(entities);
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await updateValidator.ValidateAndThrowAsync(teamUpdate);

        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);

        var missingEmployee = teamUpdate.Employees.Where((id) => !employees.Any(existing => existing.Id == id));
        if (missingEmployee.Any())
            throw new EmployeesNotFoundException(missingEmployee.ToArray());

        var existingEntity = await TeamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);
        if (existingEntity == null)
            throw new TeamNotFoundException(teamUpdate.Id);

        //var DomainEntity = Mapper.Map(teamUpdate, existingEntity);
        existingEntity.Employees = employees;
        TeamRepository.Update(existingEntity);
        await TeamRepository.SaveChangesAsync();
    }
}
