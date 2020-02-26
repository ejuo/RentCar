using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
  public interface IRepository<T>
  {
    Task SaveAsync();
  }
}