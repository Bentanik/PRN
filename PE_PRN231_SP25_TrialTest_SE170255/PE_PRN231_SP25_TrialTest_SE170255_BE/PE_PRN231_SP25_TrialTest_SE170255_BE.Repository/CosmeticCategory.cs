using Microsoft.EntityFrameworkCore;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Base;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;

public class CosmeticCategoryRepository : GenericRepository<CosmeticCategory>
{
    public CosmeticCategoryRepository() { }

    public async Task<List<CosmeticCategory>> GetAll()
    {
        var main = await _context.CosmeticCategories.ToListAsync();
        return main;
    }
}