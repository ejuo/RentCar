using RentCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentCar.DataAccess
{
    // unitofworky variant seeding data
    public class DataSeeder
    {
        public static void SeedUsers_1(RentCarDbContext context)
        {
            /*if (System.Diagnostics.Debugger.IsAttached == false)
              System.Diagnostics.Debugger.Launch();*/
            if (!context.Users.Any())
            {
                var friends = new List<User>
        {
          new User { UserName = "user1", Password = "123" },
          new User { UserName = "user2", Password = "123" }
        };
                context.AddRange(friends);
                context.SaveChanges();
            }
        }

        public static void SeedCarTypes_2(RentCarDbContext context)
        {
            if (!context.CarTypes.Any())
            {
                var carTypes = new List<CarType>
        {
          new CarType { Name = "Sedan", AccelerationTo100Secs = 10.2m, AirConditioner = true, Radio = false },
          new CarType { Name = "Sport", AccelerationTo100Secs = 5.0m, AirConditioner = false, Radio = false },
          new CarType { Name = "Vans", AccelerationTo100Secs = 15.0m, AirConditioner = true, Radio = true }
         };
                context.AddRange(carTypes);
                context.SaveChanges();
            }
        }

        public static void SeedCars_3(RentCarDbContext context)
        {
            if (!context.Cars.Any())
            {
                Random random = new Random();
                var cars = new List<Car>();
                for (int i = 1; i <= 1000; i++)
                {
                    cars.Add(new Car
                    {
                        Name = $"Car #{i}",
                        CarTypeId = random.Next(context.CarTypes.First().Id, context.CarTypes.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1),
                        AccelerationTo100Secs = Math.Round(new decimal(random.NextDouble()) * 17, 2),
                        AirConditioner = random.Next(2) == 1,
                        Radio = random.Next(2) == 1,
                        Price = Math.Round(new decimal(random.NextDouble()) * 1000, 2)
                    });
                }

                context.Cars.AddRange(cars.ToArray());

                context.SaveChanges();
            }
        }

        public static void SeedBookings_4(RentCarDbContext context)
        {
            if (!context.Bookings.Any())
            {
                Random random = new Random();
                var randomNumbers = Enumerable.Range(context.Cars.First().Id, context.Cars.OrderByDescending(x => x.Id).FirstOrDefault().Id).OrderBy(x => random.Next()).Take(6).ToList();

                var bookings = new List<Booking>
        {
          new Booking { UserId = context.Users.First().Id, CarId = randomNumbers[0], RentedAt = DateTime.Now },
          new Booking { UserId = context.Users.First().Id, CarId = randomNumbers[1], RentedAt = DateTime.Now },
          new Booking { UserId = context.Users.First().Id, CarId = randomNumbers[2], RentedAt = DateTime.Now },
          new Booking { UserId = context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id, CarId = randomNumbers[3], RentedAt = DateTime.Now },
          new Booking { UserId = context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id, CarId = randomNumbers[4], RentedAt = DateTime.Now },
          new Booking { UserId = context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id, CarId = randomNumbers[5], RentedAt = DateTime.Now }
        };

                context.Bookings.AddRange(bookings.ToArray());

                context.SaveChanges();
            }
        }
    }
}