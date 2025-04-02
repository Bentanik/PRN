using VaccinaCare.Repositories;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Services;

public interface IHealthGuidCategoryService
{
    Task<List<HealthGuideCategory>> GetAll();
    Task<HealthGuideCategory> GetById(int id);
    Task<int> Create(HealthGuideCategory healthGuideCategory);
    Task<int> Update(HealthGuideCategory healthGuideCategory);
    Task<bool> Delete(int id);
    Task<List<HealthGuideCategory>> Search(string name);

}
public class HealthGuidCategoryService : IHealthGuidCategoryService
{
    public HealthGuideCategoryRepository _repository;
    public HealthGuidCategoryService()
    {
        _repository ??= new HealthGuideCategoryRepository();
    }

    public async Task<int> Create(HealthGuideCategory healthGuideCategory)
    {
        healthGuideCategory.IsActive = true;
        return await _repository.CreateAsync(healthGuideCategory);
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

    public async Task<List<HealthGuideCategory>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<HealthGuideCategory> GetById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<HealthGuideCategory>> Search(string name)
    {
        return await _repository.Search(name);
    }

    public async Task<int> Update(HealthGuideCategory healthGuideCategory)
    {
        return await _repository.UpdateAsync(healthGuideCategory);
    }
}