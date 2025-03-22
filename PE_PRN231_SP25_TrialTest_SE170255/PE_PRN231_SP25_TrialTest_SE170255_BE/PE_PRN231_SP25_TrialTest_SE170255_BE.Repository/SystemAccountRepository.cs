using Microsoft.EntityFrameworkCore;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Base;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.Repository;

public class SystemAccountRepository : GenericRepository<SystemAccount>
{
    public SystemAccountRepository() { }

    public async Task<SystemAccount> GetUserAccount(string email, string password)
    {
        return await _context.SystemAccounts.FirstOrDefaultAsync(u => u.EmailAddress == email && u.AccountPassword == password);
    }
    public async Task<List<SystemAccount>> GetAll()
    {
        var users = await _context.SystemAccounts.ToListAsync();
        return users;
    }
}
