using RentCar.Domain.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Services.PreflightService
{
  /// <summary>
  /// Preflight car check rent service.
  /// </summary>
  public class PreflightService : IPreflightService
  {
    private IBookingRepository _bookingRepository;

    /// <summary>
    /// For rounding acceletationTo values.
    /// </summary>
    private const decimal MEASUREMENT_ERROR = 5.0m;
    public PreflightService(IBookingRepository bookingRepository)
    {
      _bookingRepository = bookingRepository;
    }

    public async Task<PreflightResult> Preflight(Car selectedCar)
    {
      if (await _bookingRepository.HasRentAsync(selectedCar.Id))
      {
        return new PreflightResult
        {
          Code = PreflightResultCode.BLOCKED,
          Message = string.Format(PreflightResultMessage.BLOCKED, selectedCar.Name)
        };
      }

      var resultCode = PreflightResultCode.OK;

      var preFlightMessage = new StringBuilder();
      if (selectedCar.AccelerationTo100Secs - selectedCar.CarType.AccelerationTo100Secs > MEASUREMENT_ERROR)
      {
        preFlightMessage.Append(PreflightResultMessage.ACCELERATION).AppendLine();
        resultCode = PreflightResultCode.WARNING;
      }
      if (!selectedCar.AirConditioner && selectedCar.CarType.AirConditioner)
      {
        preFlightMessage.Append(PreflightResultMessage.AIRCONDITIONER).AppendLine();
        resultCode = PreflightResultCode.WARNING;
      }
      if (!selectedCar.Radio && selectedCar.CarType.Radio)
      {
        preFlightMessage.Append(PreflightResultMessage.RADIO).AppendLine();
        resultCode = PreflightResultCode.WARNING;
      }

      preFlightMessage.Append(string.Format(PreflightResultMessage.APPROVE, selectedCar.Name));

      return new PreflightResult
      {
        Code = resultCode,
        Message = preFlightMessage.ToString()
      };
    }
  }
}
