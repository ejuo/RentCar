using Prism.Events;

namespace RentCar.UI.Event
{
  public class OpenViewEvent : PubSubEvent<OpenViewEventArgs>
  {
  }
  public class OpenViewEventArgs
  {
    public string ViewModelName { get; set; }
  }
}
