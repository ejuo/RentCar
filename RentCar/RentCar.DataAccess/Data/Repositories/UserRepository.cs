using Microsoft.EntityFrameworkCore;
using RentCar.Domain;
using RentCar.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories
{
    public class UserRepository : Repository<User, RentCarDbContext>,
    IUserRepository
    {
        public UserRepository(RentCarDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await Context.Users
              .Include(m => m.Bookings).ToListAsync();
        }
    }
}
