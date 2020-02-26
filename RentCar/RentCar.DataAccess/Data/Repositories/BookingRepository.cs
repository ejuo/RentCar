using Microsoft.EntityFrameworkCore;
using RentCar.Domain;
using RentCar.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories
{
    public class BookingRepository : Repository<Booking, RentCarDbContext>,
      IBookingRepository
    {
        public BookingRepository(RentCarDbContext context) : base(context)
        {
        }

        public void MakeRent(Booking booking)
        {
            Context.Bookings.Add(booking);
        }

        public async Task<bool> HasRentAsync(int carId)
        {
            return await Context.Bookings.AnyAsync(b => b.CarId == carId && (b.RentDoneAt >= DateTime.Now || b.RentDoneAt == null));
        }
    }
}
