
namespace RentCar.Domain
{
  public class FilterData
  {
    public int? CarTypeId { get; set; }
    public bool? AirConditioner { get; set; }
    public bool? Radio { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }

  }
}
