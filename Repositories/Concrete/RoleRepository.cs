using System;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Concrete;
using Repositories.Contract;

namespace Repositories.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ePizzaHubContext dbContext) : base(dbContext)
        {
        }
    }
}
