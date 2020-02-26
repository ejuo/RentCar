using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Domain
{
  public class User
  {
    public User()
    {
      Bookings = new Collection<Booking>();
    }

    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public ICollection<Booking> Bookings { get; set; }
  }
}
