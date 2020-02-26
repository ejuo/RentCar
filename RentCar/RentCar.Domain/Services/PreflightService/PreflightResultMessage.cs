namespace RentCar.Domain.Services.PreflightService
{
  public static class PreflightResultMessage
  {
    public const string BLOCKED = "{0} can't be rented, because already rented";
    public const string ACCELERATION = "Engine is not working well";
    public const string AIRCONDITIONER = "Air conditioning is not working well";
    public const string RADIO = "Radio is not working well";
    public const string APPROVE = "Do you really want to rent the car {0}?";
  }
}
