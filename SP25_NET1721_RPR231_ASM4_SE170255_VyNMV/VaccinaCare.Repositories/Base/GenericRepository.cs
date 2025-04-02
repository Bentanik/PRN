using Microsoft.EntityFrameworkCore;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Repositories.Base;

public class GenericRepository<T> where T : class
{
    protected SP25_SE1721_PRN231_GR5_VaccinaCareContext _context;

    public GenericRepository()
    {
        _context ??= new SP25_SE1721_PRN231_GR5_VaccinaCareContext();
    }

    public GenericRepository(SP25_SE1721_PRN231_GR5_VaccinaCareContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public void Create(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public async Task<int> CreateAsync(T entity)
    {
        _context.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        try
        {
            // Get primary key dynamically
            var keyValues = _context.Model.FindEntityType(typeof(T))
                             ?.FindPrimaryKey()
                             ?.Properties
                             ?.Select(p => p.PropertyInfo.GetValue(entity))
                             .ToArray();

            if (keyValues == null || keyValues.Length == 0)
                throw new InvalidOperationException("No primary key defined for entity.");

            // Fetch existing entity without tracking
            var existingEntity = await _context.Set<T>().FindAsync(keyValues);

            if (existingEntity == null) return 0;

            _context.Entry(existingEntity).State = EntityState.Detached; // ✅ Prevent tracking conflicts
            _context.Entry(entity).State = EntityState.Modified; // ✅ Mark for update

            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public bool Remove(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public T GetById(int id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public T GetById(string code)
    {
        var entity = _context.Set<T>().Find(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(string code)
    {
        var entity = await _context.Set<T>().FindAsync(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public T GetById(Guid code)
    {
        var entity = _context.Set<T>().Find(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(Guid code)
    {
        var entity = await _context.Set<T>().FindAsync(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }
}
