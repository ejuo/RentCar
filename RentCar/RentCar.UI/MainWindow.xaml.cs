using RentCar.UI.ViewModel;
using System.Windows;

namespace RentCar.UI
{
  public partial class MainWindow : Window
  {
    private MainViewModel _viewModel;

    public MainWindow(MainViewModel viewModel)
    {
      InitializeComponent();
      _viewModel = viewModel;
      DataContext = _viewModel;
    }
  }
}
