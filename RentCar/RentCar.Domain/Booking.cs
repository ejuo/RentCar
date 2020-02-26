using System;

namespace RentCar.Domain
{
  public class Booking
  {
    public Booking() { }
    public int Id { get; set; }
    public DateTime RentedAt { get; set; }
    public DateTime? RentDoneAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
  }
}
