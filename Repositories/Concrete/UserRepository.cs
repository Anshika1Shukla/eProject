using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ePizzaHubContext dbContext) : base(dbContext)
    {
    }

    public async  Task<User> FindByUserNameAsync(string userName)
    {
        return await _dbContext.
            Users.
             Include(x=>x.Roles).
             FirstOrDefaultAsync(x => x.Email == userName);
    }
}
