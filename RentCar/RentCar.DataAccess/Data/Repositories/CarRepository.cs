using Microsoft.EntityFrameworkCore;
using RentCar.Domain;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.DataAccess.Data.Repositories
{
    public class CarRepository : Repository<Car, RentCarDbContext>,
    ICarRepository
    {
        public CarRepository(RentCarDbContext context) : base(context)
        {
        }

        public async Task<List<Car>> GetByFilterAsync(FilterData filterData)
        {
            var query = Context.Cars.Include(m => m.Bookings).Where(s => s.Bookings.
                    FirstOrDefault(b => b.RentDoneAt == null || b.RentDoneAt > DateTime.Now) == null)
              .Include(m => m.CarType).AsQueryable();

            if (filterData != null)
            {
                query = query.Where(c =>
                            (filterData.AirConditioner == null || c.CarType.AirConditioner == filterData.AirConditioner)
                         && (filterData.Radio == null || c.CarType.Radio == filterData.Radio)
                         && (filterData.PriceMin == null || c.Price >= filterData.PriceMin)
                         && (filterData.PriceMax == null || c.Price <= filterData.PriceMax)
                         && (filterData.CarTypeId == null || c.CarTypeId == filterData.CarTypeId)
                );
            }

            return await query.ToListAsync();
        }
    }
}
