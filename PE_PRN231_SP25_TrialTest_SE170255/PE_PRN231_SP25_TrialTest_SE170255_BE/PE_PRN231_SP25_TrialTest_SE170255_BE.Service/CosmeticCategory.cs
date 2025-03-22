using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Service;

public interface ICosmeticCategoryService
{
    Task<List<CosmeticCategory>> GetAll();
}

public class CosmeticCategoryService : ICosmeticCategoryService
{
    public CosmeticCategoryRepository _repo;
    public CosmeticCategoryService()
    {
        _repo = new CosmeticCategoryRepository();
    }

    public async Task<List<CosmeticCategory>> GetAll()
    {
        return await _repo.GetAll();
    }
}
