using System.Threading.Tasks;

namespace RentCar.UI.ViewModel
{
  /// <summary>
  /// Common interface for collecting Views.
  /// </summary>
  public interface IViewModel
  {
    /// <summary>
    /// Load with prepared data from parent.
    /// </summary>
    /// <param name="preparedData"></param>
    /// <returns></returns>
    Task LoadAsync(object preparedData = null);
  }
}