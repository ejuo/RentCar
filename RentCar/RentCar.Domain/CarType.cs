using System.ComponentModel.DataAnnotations;

namespace RentCar.Domain
{
  public class CarType
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public decimal AccelerationTo100Secs { get; set; }
    public bool AirConditioner { get; set; }
    public bool Radio { get; set; }
  }
}