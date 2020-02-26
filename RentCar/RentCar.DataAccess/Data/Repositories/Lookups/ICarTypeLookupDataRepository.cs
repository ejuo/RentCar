using RentCar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories.Lookups
{
  public interface ICarTypeLookupDataRepository
  {
    Task<List<LookupItem>> GetCarTypeLookupAsync();
  }
}