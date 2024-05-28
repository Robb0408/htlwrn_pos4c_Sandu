using System.Windows;
using Seats.Models;
using Seats.ViewModels;

namespace Seats;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModel = new MainWindowViewModel();
    }

    public MainWindowViewModel ViewModel { get; }
}