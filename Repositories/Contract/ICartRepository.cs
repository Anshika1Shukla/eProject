using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<int> GetCartItemQuantityAsync(Guid guid);
        Task<Cart> GetCartDetailsAsync(Guid guid);


    }
}
