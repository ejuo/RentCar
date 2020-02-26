using RentCar.Domain;

namespace RentCar.UI.Wrapper
{
    public class CarWrapper : ModelWrapper<Car>
    {
        public CarWrapper(Car model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
        }

        public string CarTypeAirConditioner
        {
            get { return Model.CarType.AirConditioner ? "Yes" : "No"; }
        }

        public string CarTypeRadio
        {
            get { return Model.CarType.Radio ? "Yes" : "No"; }
        }

        public decimal Price
        {
            get { return GetValue<decimal>(); }
        }

        public string CarTypeName
        {
            get { return Model.CarType.Name; }
        }
    }
}
