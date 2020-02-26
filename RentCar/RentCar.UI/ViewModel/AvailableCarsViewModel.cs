using Prism.Commands;
using RentCar.DataAccess.Data.Repositories.Lookups;
using RentCar.Domain;
using RentCar.Domain.Interfaces;
using RentCar.Domain.Services.PreflightService;
using RentCar.UI.Services.MessageDialogService;
using RentCar.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentCar.UI.ViewModel
{
    /// <summary>
    /// View with available cars for rent.
    /// </summary>
    public class AvailableCarsViewModel : ViewModelBase, IViewModel
    {
        private ICarRepository _carRepository;

        private IBookingRepository _bookingRepository;

        private IMessageDialogService _messageDialogService;

        private User _currentUser;

        private CarWrapper _selectedCar;

        private FilterDataWrapper _filterData;

        private ICarTypeLookupDataRepository _carTypeLookupDataService;

        private IPreflightService _preflightService;


        public AvailableCarsViewModel(IMessageDialogService messageDialogService, IPreflightService preflightService,
            ICarTypeLookupDataRepository carTypeLookupDataService, ICarRepository carRepository, IBookingRepository bookingRepository)
        {
            _carTypeLookupDataService = carTypeLookupDataService;
            _carRepository = carRepository;
            _bookingRepository = bookingRepository;
            _messageDialogService = messageDialogService;
            _preflightService = preflightService;

            FilterCommand = new DelegateCommand(OnFilterExecute);
            RentCommand = new DelegateCommand(OnRentExecute, OnRentCanExecute);
            CarTypes = new ObservableCollection<LookupItem>();
            Cars = new ObservableCollection<CarWrapper>();
        }

        public CarWrapper SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                ((DelegateCommand)RentCommand).RaiseCanExecuteChanged();
            }
        }

        public FilterDataWrapper FilterData
        {
            get { return _filterData; }
            private set
            {
                _filterData = value;
                OnPropertyChanged();
            }
        }

        public ICommand FilterCommand { get; }
        public ICommand RentCommand { get; }

        public ObservableCollection<CarWrapper> Cars { get; }

        public ObservableCollection<LookupItem> CarTypes { get; }

        /// <summary>
        /// Load View by logined user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task LoadAsync(object user)
        {
            _currentUser = (User)user;
            var cars = await _carRepository.GetByFilterAsync();

            InitializeAvailableCars(cars);
            InitializeDataFilter(new FilterData());
            await LoadCarTypeLookupAsync();
        }

        private async Task LoadCarTypeLookupAsync()
        {
            CarTypes.Clear();
            CarTypes.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _carTypeLookupDataService.GetCarTypeLookupAsync();
            foreach (var lookupItem in lookup)
            {
                CarTypes.Add(lookupItem);
            }
        }

        protected async void OnFilterExecute()
        {
            var cars = await _carRepository.GetByFilterAsync(FilterData.Model);

            InitializeAvailableCars(cars);
        }

        private async void OnRentExecute()
        {
            var selectedCar = SelectedCar;

            var preflight = await _preflightService.Preflight(SelectedCar.Model);

            if (preflight.Code == PreflightResultCode.BLOCKED)
            {
                _messageDialogService.ShowInfoDialog(preflight.Message);
                Cars.Remove(SelectedCar);
                SelectedCar = null;
                return;
            }

            var result = _messageDialogService.ShowOkCancelDialog(preflight.Message, "Confirmation");
            if (result == MessageDialogResult.OK)
            {
                var booking = new Booking { CarId = SelectedCar.Model.Id, UserId = _currentUser.Id, RentedAt = DateTime.Now };
                _bookingRepository.MakeRent(booking);
                await _bookingRepository.SaveAsync();
                Cars.Remove(SelectedCar);
                SelectedCar = null;
                _messageDialogService.ShowInfoDialog($"You successfuly rented {selectedCar.Name}");
            }
        }

        private bool OnRentCanExecute()
        {
            return SelectedCar != null;
        }

        private void InitializeAvailableCars(ICollection<Car> cars)
        {
            Cars.Clear();
            foreach (var car in cars)
            {
                var wrapper = new CarWrapper(car);
                Cars.Add(wrapper);
            }
        }

        private void InitializeDataFilter(FilterData filterData)
        {
            FilterData = new FilterDataWrapper(filterData);
        }
    }
}
