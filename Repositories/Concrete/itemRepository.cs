using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contract;

namespace ePizzaHub.Repositories.Concrete
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(ePizzaHubContext dbContext) : base(dbContext)
        {
        }
    }
}
