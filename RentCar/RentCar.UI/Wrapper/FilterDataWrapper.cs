using RentCar.Domain;

namespace RentCar.UI.Wrapper
{
    public class FilterDataWrapper : ModelWrapper<FilterData>
    {
        public FilterDataWrapper(FilterData model) : base(model) { }
        public int? CarTypeId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }
        public bool? AirConditioner
        {
            get { return GetValue<bool?>(); }
            set { SetValue(value); }
        }

        public bool? Radio
        {
            get { return GetValue<bool?>(); }
            set { SetValue(value); }
        }

        public decimal? PriceMin
        {
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }

        public decimal? PriceMax
        {
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }
    }
}
