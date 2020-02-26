using System.Threading.Tasks;

namespace RentCar.Domain.Services.PreflightService
{
  /// <summary>
  /// Preflight car check rent service.
  /// </summary>
  public interface IPreflightService
  {
    Task<PreflightResult> Preflight(Car selectedCar);
  }
}