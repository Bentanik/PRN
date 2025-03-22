using VaccinaCare.Repositories;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Services;

public interface IHealthGuidCategoryService
{
    Task<List<HealthGuideCategory>> GetAll();
}
public class HealthGuidCategoryService : IHealthGuidCategoryService
{
    public HealthGuideCategoryRepository _repository;
    public HealthGuidCategoryService()
    {
        _repository ??= new HealthGuideCategoryRepository();
    }
    public async Task<List<HealthGuideCategory>> GetAll()
    {
        return await _repository.GetAll();
    }
}