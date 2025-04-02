using VaccinaCare.Repositories;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Services;

public interface IHealthGuidService
{
    Task<List<HealthGuide>> GetAll();
    Task<HealthGuide> GetById(int id);
    Task<int> Create(HealthGuide schedule);
    Task<int> Update(HealthGuide schedule);
    Task<bool> Delete(int id);
    Task<List<HealthGuide>> Search(string expertResponse, string location, string name);
}

public class HealthGuidService : IHealthGuidService
{
    private readonly HealthGuideRepository _repository;
    public HealthGuidService()
    {
        _repository = new HealthGuideRepository();
    }
    public async Task<int> Create(HealthGuide healthGuid)
    {
        healthGuid.CreatedAt = DateTime.UtcNow;
        healthGuid.UpdatedAt = DateTime.UtcNow;
        healthGuid.IsActive = true;
        return await _repository.CreateAsync(healthGuid);
    }

    public async Task<bool> Delete(int id)
    {
        var schedule = await _repository.GetByIdAsync(id);
        if (schedule != null)
        {
            return await _repository.RemoveAsync(schedule);
        }
        return false;
    }
    
    public async Task<List<HealthGuide>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<HealthGuide> GetById(int id)
    {
        return await _repository.GetByIdIncludeAsync(id);
    }

    public async Task<List<HealthGuide>> Search(string expertResponse, string location, string name)
    {
        return await _repository.Search(expertResponse, location, name);
    }

    public async Task<int> Update(HealthGuide healthGuid)
    {
        healthGuid.CreatedAt = DateTime.UtcNow;
        healthGuid.UpdatedAt = DateTime.UtcNow;
        healthGuid.IsActive = true;
        return await _repository.UpdateAsync(healthGuid);
    }
}