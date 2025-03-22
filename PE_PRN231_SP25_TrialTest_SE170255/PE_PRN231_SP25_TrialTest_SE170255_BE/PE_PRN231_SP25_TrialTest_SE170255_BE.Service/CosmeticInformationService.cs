using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Service;

public interface ICosmeticInformationService
{
    Task<List<CosmeticInformation>> GetAll();
    Task<CosmeticInformation> GetById(string id);
    Task<int> Create(CosmeticInformation cosmeticInformation);
    Task<int> Update(CosmeticInformation cosmeticInformation);
    Task<bool> Delete(string id);
    Task<List<CosmeticInformation>> Search(string name, string size, string skinType);
}

public class CosmeticInformationService : ICosmeticInformationService
{
    public CosmeticInformationRepository _repo;
    public CosmeticInformationService()
    {
        _repo = new CosmeticInformationRepository();
    }
    public async Task<int> Create(CosmeticInformation cosmeticInformation)
    {
        return await _repo.CreateAsync(cosmeticInformation);
    }

    public async Task<bool> Delete(string id)
    {
        var cosmeticInformation = await _repo.GetByIdAsync(id);
        if (cosmeticInformation != null)
        {
            return await _repo.RemoveAsync(cosmeticInformation);
        }
        return false;
    }

    public async Task<List<CosmeticInformation>> GetAll()
    {
        return await _repo.GetAll();
    }

    public async Task<CosmeticInformation> GetById(string id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<List<CosmeticInformation>> Search(string name, string size, string skinType)
    {
        return await _repo.Search(name, size, skinType);
    }

    public async Task<int> Update(CosmeticInformation cosmeticInformation)
    {
        return await _repo.UpdateAsync(cosmeticInformation);
    }
}
