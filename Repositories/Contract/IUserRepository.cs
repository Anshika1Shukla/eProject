using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contract;

namespace Repositories.Contract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByUserNameAsync(string userName);

    }
}
