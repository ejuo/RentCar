using RentCar.UI.Services.MessageDialogService;
using System.Windows;

namespace RentCar.UI.View.Services
{
    /// <summary>
    /// Message dialog service. 
    /// For mocking.
    /// </summary>
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
              ? MessageDialogResult.OK
              : MessageDialogResult.Cancel;
        }
        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Info");
        }

    }
}
