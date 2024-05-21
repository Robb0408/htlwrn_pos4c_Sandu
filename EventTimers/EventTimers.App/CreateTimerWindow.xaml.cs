using EventTimers.App.ViewModels;
using System.Windows;

namespace EventTimers.App
{
    /// <summary>
    /// Interaktionslogik für CreateTimerWindow.xaml
    /// </summary>
    public partial class CreateTimerWindow : Window
    {
        public CreateTimerWindow()
        {
            InitializeComponent();
            submitBtn.Click += (s, e) => DialogResult = true;
        }
    }
}
