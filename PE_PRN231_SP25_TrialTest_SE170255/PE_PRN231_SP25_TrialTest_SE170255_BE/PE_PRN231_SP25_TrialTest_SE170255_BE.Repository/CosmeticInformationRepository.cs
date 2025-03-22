using Microsoft.EntityFrameworkCore;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Base;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;

public class CosmeticInformationRepository : GenericRepository<CosmeticInformation>
{
    public CosmeticInformationRepository() { }

    public async Task<List<CosmeticInformation>> GetAll()
    {
        var main = await _context.CosmeticInformations.Include(a => a.Category).ToListAsync();
        return main;
    }

    public async Task<CosmeticInformation> GetByIdAsync(string id)
    {
        var main = await _context.CosmeticInformations.Include(t => t.Category).FirstOrDefaultAsync(t => t.CosmeticId == id);
        return main;
    }

    public async Task<List<CosmeticInformation>> Search(string name, string size, string skinType)
    {
        var mains = await _context.CosmeticInformations
            .Where(ta =>
            (ta.CosmeticName.Contains(name) || string.IsNullOrEmpty(name))
            && (ta.CosmeticSize.Contains(size) || string.IsNullOrEmpty(size))
            && (ta.SkinType.Contains(skinType) || string.IsNullOrEmpty(skinType))
            ).ToListAsync();

        return mains;
    }
}
