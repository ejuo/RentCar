using Microsoft.EntityFrameworkCore;
using RentCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentCar.DataAccess
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options)
        : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relations
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CarId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(fm => fm.UserId);

            // Remove PluralizingTableNameConvention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }


            // Seeding
            modelBuilder.Entity<User>()
              .HasData(new { Id = 1, UserName = "user1", Password = "123" },
                      new { Id = 2, UserName = "user2", Password = "123" }
              );
            modelBuilder.Entity<CarType>()
              .HasData(new { Id = 1, Name = "Sedan", AccelerationTo100Secs = 10.2m, AirConditioner = true, Radio = false },
                new { Id = 2, Name = "Sport", AccelerationTo100Secs = 5.0m, AirConditioner = false, Radio = false },
                new { Id = 3, Name = "Vans", AccelerationTo100Secs = 15.0m, AirConditioner = true, Radio = true }
              );

            Random random = new Random();
            var cars = new List<Car>();
            for (int i = 1; i <= 1000; i++)
            {
                cars.Add(new Car
                {
                    Id = i,
                    Name = $"Car #{i}",
                    CarTypeId = random.Next(1, 4),
                    AccelerationTo100Secs = Math.Round(new decimal(random.NextDouble()) * 17, 2),
                    AirConditioner = random.Next(2) == 1,
                    Radio = random.Next(2) == 1,
                    Price = Math.Round(new decimal(random.NextDouble()) * 1000, 2)
                });
            }
            modelBuilder.Entity<Car>()
              .HasData(cars.Select(x => new
              {
                  Id = x.Id,
                  Name = x.Name,
                  CarTypeId = x.CarTypeId,
                  AccelerationTo100Secs = x.AccelerationTo100Secs,
                  AirConditioner = x.AirConditioner,
                  Radio = x.Radio,
                  Price = x.Price
              }).ToArray());

            var randomNumbers = Enumerable.Range(1, 1000).OrderBy(x => random.Next()).Take(6).ToList();

            modelBuilder.Entity<Booking>()
              .HasData(new { Id = 1, UserId = 1, CarId = randomNumbers[0], RentedAt = DateTime.Now },
                new { Id = 2, UserId = 1, CarId = randomNumbers[1], RentedAt = DateTime.Now },
                new { Id = 3, UserId = 1, CarId = randomNumbers[2], RentedAt = DateTime.Now },
                new { Id = 4, UserId = 2, CarId = randomNumbers[3], RentedAt = DateTime.Now },
                new { Id = 5, UserId = 2, CarId = randomNumbers[4], RentedAt = DateTime.Now },
                new { Id = 6, UserId = 2, CarId = randomNumbers[5], RentedAt = DateTime.Now }
              );

        }
    }
}