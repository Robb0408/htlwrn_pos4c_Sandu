using EventTimers.App.ViewModels;
using System.Windows;
namespace EventTimers.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel viewModel;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = viewModel = new MainViewModel();
    }
}