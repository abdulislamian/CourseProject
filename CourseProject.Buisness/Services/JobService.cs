using AutoMapper;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using CourseProject.Buisness.Exceptions;
using CourseProject.Buisness.Validation;
using CourseProject.Common.Interfaces;

namespace Courseproject.Business.Services;

public class JobService : IJobService
{
    private readonly JobCreateValidator createValidator;
    private readonly JobUpdateValidator updateValidator;

    private IMapper Mapper { get; }
    private IGenericRepository<Job> JobRepository { get; }

    public JobService(IMapper mapper, IGenericRepository<Job> jobRepository,JobCreateValidator _createValidator, JobUpdateValidator _updateValidator)
    {
        Mapper = mapper;
        JobRepository = jobRepository;
        createValidator = _createValidator;
        updateValidator = _updateValidator;
    }


    public async Task<int> CreateJobAsync(JobCreate jobCreate)
    {
        var entity = Mapper.Map<Job>(jobCreate);
        await JobRepository.InsertAsync(entity);
        await JobRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteJobAsync(JobDelete jobDelete)
    {
        var entity = await JobRepository.GetByIdAsync(jobDelete.Id,(jobs)=>jobs.Employees);

        if (entity == null)
            throw new JobNotFoundException(jobDelete.Id);
        if (entity.Employees.Count > 0)
            throw new DependentEmployeesExistException(entity.Employees);


        JobRepository.Delete(entity);
        await JobRepository.SaveChangesAsync();
    }

    public async Task<JobGet> GetJobAsync(int id)
    {
        var entity = await JobRepository.GetByIdAsync(id);
        if(entity == null)
              throw new JobNotFoundException(id);
        return Mapper.Map<JobGet>(entity);
    }

    public async Task<List<JobGet>> GetJobsAsync()
    {
        var entities = await JobRepository.GetAsync(null, null);
        return Mapper.Map<List<JobGet>>(entities);
    }

    public async Task UpdateJobAsync(JobUpdate jobUpdate)
    {
        var entity = await JobRepository.GetByIdAsync(jobUpdate.Id);
        if (entity == null)
            throw new JobNotFoundException(jobUpdate.Id);


        var Domainentity = Mapper.Map<Job>(jobUpdate);
        JobRepository.Update(Domainentity);
        await JobRepository.SaveChangesAsync();
    }
}
