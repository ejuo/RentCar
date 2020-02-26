using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Domain
{
  public class Car
  {
    public Car()
    {
      Bookings = new Collection<Booking>();
    }
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public int CarTypeId { get; set; }
    public CarType CarType { get; set; }
    public decimal AccelerationTo100Secs { get; set; }
    public bool AirConditioner { get; set; }
    public bool Radio { get; set; }
    public decimal Price { get; set; }
    public ICollection<Booking> Bookings { get; set; }
  }
}
