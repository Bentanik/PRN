using Microsoft.EntityFrameworkCore;
using VaccinaCare.Repositories.Base;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository() { }
    public async Task<User> GetUserAccount(string email, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
    }
}
