namespace RentCar.UI.Services.MessageDialogService
{
    /// <summary>
    /// Message dialog service.
    /// </summary>
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
        void ShowInfoDialog(string text);
    }
}