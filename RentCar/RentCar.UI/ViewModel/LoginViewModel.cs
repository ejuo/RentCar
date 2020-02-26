using Prism.Commands;
using Prism.Events;
using RentCar.Domain.Interfaces;
using RentCar.UI.Event;
using RentCar.UI.Services.MessageDialogService;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentCar.UI.ViewModel
{
    /// <summary>
    /// Login.
    /// </summary>
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public ICommand LoginCommand { get; set; }

        private IEventAggregator _eventAggregator;

        private IMessageDialogService _messageDialogService;

        private IUserRepository _userRepository;

        public LoginViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
          IUserRepository userRepository)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _userRepository = userRepository;

            LoginCommand = new DelegateCommand<object>(Login);
        }

        public Task LoadAsync(object preparedData)
        {
            return Task.FromResult<object>(null);
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private void Login(object userInput)
        {
            CheckLogin(_userName, ((PasswordBox)userInput).Password);
        }

        private async void CheckLogin(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (user != null)
            {
                _eventAggregator.GetEvent<LoginSuccessEvent>().Publish(user);
            }
            else
            {
                _messageDialogService.ShowInfoDialog("username or password is incorrect");
            }
        }
    }
}
