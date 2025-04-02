using Microsoft.EntityFrameworkCore;
using VaccinaCare.Repositories.Base;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Repositories;

public class HealthGuideRepository : GenericRepository<HealthGuide>
{
    public HealthGuideRepository() { }

    public async Task<List<HealthGuide>> GetAll()
    {
        var entities = await _context.HealthGuides.Include(x => x.HealthGuideCategorie).ToListAsync();
        return entities;
    }

    public async Task<HealthGuide> GetByIdIncludeAsync(int id)
    {
        var entity = await _context.HealthGuides.Include(x => x.HealthGuideCategorie).FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<List<HealthGuide>> Search(string title, string content, string name)
    {
        var schedules = await _context.HealthGuides
            .Include(st => st.HealthGuideCategorie)
            .Where(s =>
            (s.Title.Contains(title) || string.IsNullOrEmpty(title)
            && (s.Content.Contains(content) || string.IsNullOrEmpty(content))
            && (s.HealthGuideCategorie.Name.Contains(name) || string.IsNullOrEmpty(name)
            ))).ToListAsync();

        return schedules;
    }
}