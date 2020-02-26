using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
  public interface IUserRepository : IRepository<User>
  {
    Task<List<User>> GetAllAsync();
  }
}