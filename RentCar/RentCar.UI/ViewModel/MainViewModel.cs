using Autofac.Features.Indexed;
using Prism.Events;
using RentCar.Domain;
using RentCar.UI.Event;
using RentCar.UI.Services.MessageDialogService;

namespace RentCar.UI.ViewModel
{
  /// <summary>
  /// Main.
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    private IEventAggregator _eventAggregator;
    private IMessageDialogService _messageDialogService;
    private IIndex<string, IViewModel> _viewModelCreator;

    private IViewModel _viewModel;
    private string _userMenuHeader;

    public MainViewModel(
      IIndex<string, IViewModel> viewModelCreator,
      IEventAggregator eventAggregator,
      IMessageDialogService messageDialogService)
    {
      _eventAggregator = eventAggregator;
      _viewModelCreator = viewModelCreator;
      _messageDialogService = messageDialogService;

      _eventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(HandleLoginSuccess);

      ViewModel = _viewModelCreator[nameof(LoginViewModel)];
    }

    public IViewModel ViewModel
    {
      get { return _viewModel; }
      private set
      {
        _viewModel = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// See the current user in the corner of the window.
    /// </summary>
    public string UserMenuHeader
    {
      get => _userMenuHeader;
      set
      {
        _userMenuHeader = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Captures a successful login event and show next view.
    /// </summary>
    /// <param name="user"></param>
    private async void HandleLoginSuccess(User user)
    {
      ViewModel = _viewModelCreator[nameof(AvailableCarsViewModel)];
      await ViewModel.LoadAsync(user);
      UserMenuHeader = user.UserName;
    }
  }
}
