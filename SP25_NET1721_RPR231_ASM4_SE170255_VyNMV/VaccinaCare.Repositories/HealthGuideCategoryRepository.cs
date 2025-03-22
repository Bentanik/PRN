using Microsoft.EntityFrameworkCore;
using VaccinaCare.Repositories.Base;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Repositories;

public class HealthGuideCategoryRepository : GenericRepository<HealthGuideCategoryRepository>
{
    public HealthGuideCategoryRepository() { }

    public async Task<List<HealthGuideCategory>> GetAll()
    {
        var entities = await _context.HealthGuideCategories.ToListAsync();
        return entities;
    }
}
