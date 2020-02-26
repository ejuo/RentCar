using Prism.Events;
using RentCar.Domain;

namespace RentCar.UI.Event
{
  public class LoginSuccessEvent : PubSubEvent<User> { }
}
