using RentCar.UI.ViewModel;
using System.Runtime.CompilerServices;

namespace RentCar.UI.Wrapper
{
    /// <summary>
    /// Base Model Wrapper class for tracking props.
    /// </summary>
    public class ModelWrapper<T> : ViewModelBase
    {
        public ModelWrapper(T model)
        {
            Model = model;
        }

        public T Model { get; }

        protected virtual void SetValue<TValue>(TValue value,
          [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName)?.SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName)?.GetValue(Model);
        }
    }
}
