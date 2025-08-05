using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ePizzaHubContext dbContext) : base(dbContext)
        {
        }
        public async Task<Cart> GetCartDetailsAsync(Guid guid)
        {
            return await _dbContext
                       .Carts
                       .Include(x => x.CartItems)
                       .ThenInclude(x => x.Item)
                       .Where(x => x.Id == guid && x.IsActive == true)
                       .FirstOrDefaultAsync();
        }

        public async Task<int> GetCartItemQuantityAsync(Guid guid)
        {
            int itemCount = await _dbContext.CartItems.Where(x => x.CartId == guid).CountAsync();
            return itemCount;
        }
    }
}
