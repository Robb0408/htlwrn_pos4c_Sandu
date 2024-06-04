using FitnessTracker.App.Models;
using FitnessTracker.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessTracker.App
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private ActivityViewModel? viewModel;

        public bool IsClosingAllowed { get; set; } = false;

        public ActivityWindow()
        {
            InitializeComponent();

        }

        public void LoadActivity(Activity activity)
        {
            DataContext = viewModel =
            App.Current.Services.GetService<ActivityViewModel>()!;
            // handover the activity to the ViewModel
            viewModel?.LoadActivity(activity);
        }
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsClosingAllowed)
            {
                e.Cancel = true;
            }
        }
    }
}
