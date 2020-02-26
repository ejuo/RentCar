using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
  public interface ICarRepository : IRepository<Car>
  {
    Task<List<Car>> GetByFilterAsync(FilterData filterData = null);
  }
}