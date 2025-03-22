using Microsoft.EntityFrameworkCore;
using VaccinaCare.Repositories.Base;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Repositories;

public class HealthGuideCategoryRepository : GenericRepository<HealthGuideCategory>
{
    public HealthGuideCategoryRepository() { }

    public async Task<List<HealthGuideCategory>> GetAll()
    {
        var entities = await _context.HealthGuideCategories.ToListAsync();
        return entities;
    }

    public async Task<List<HealthGuideCategory>> Search(string name)
    {
        var items = await _context.HealthGuideCategories
            .Where(s => s.Name.Contains(name) || string.IsNullOrEmpty(name))
            .ToListAsync();

        return items;
    }
}
