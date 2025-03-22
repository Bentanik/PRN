using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Service;

public interface ISystemAccountService
{
    Task<SystemAccount> Authenticate(string email, string password);
    Task<List<SystemAccount>> GetAll();
}
public class SystemAccountService : ISystemAccountService
{
    private readonly SystemAccountRepository _repository;

    public SystemAccountService()
    {
        _repository = new SystemAccountRepository();
    }

    public async Task<SystemAccount> Authenticate(string email, string password)
    {
        return await _repository.GetUserAccount(email, password);
    }

    public async Task<List<SystemAccount>> GetAll()
    {
        return await _repository.GetAll();
    }
}
