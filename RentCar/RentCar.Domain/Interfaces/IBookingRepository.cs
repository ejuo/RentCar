using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
  public interface IBookingRepository : IRepository<Booking>
  {
    void MakeRent(Booking booking);
    Task<bool> HasRentAsync(int carId);
  }
}