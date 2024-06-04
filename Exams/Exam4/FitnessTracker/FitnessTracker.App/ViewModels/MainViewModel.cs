using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessTracker.App.Models;
using FitnessTracker.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
// HAU: ℹ️ remove not used usings
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

// HAU: ℹ️ use file scoped namespace (single line) declaration to reduce code nesting
namespace FitnessTracker.App.ViewModels
{
    // HAU: ⚠️ add breaks after fields and properties to enhance readability
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<Activity> activities = new();
        public ObservableCollection<Activity> Activities
        {
            get => activities;
            set => SetProperty(ref activities, value);
        }

        private string? trackingText;
        public string TrackingText
        {
            get => trackingText ?? string.Empty;
            set => SetProperty(ref trackingText, value);
        }

        private string? btnText;
        public string BtnText
        {
            get => btnText ?? string.Empty;
            set => SetProperty(ref btnText, value);
        }

        private string? selectedSport;
        public string SelectedSport
        {
            get => selectedSport ?? string.Empty;
            set => SetProperty(ref selectedSport, value);
        }

        public List<string> SportTypes { get; set; } = new List<string> { "Running", "Cycling" };
        // HAU: ⚠️ you do not need command parameters here use the bound SelectedItem property of the DataGrid instead
        public RelayCommand<Activity> DeleteActivityCommand { get; }

        public RelayCommand StartStopTrackingCommand { get; }
        public ActivityWindow? ActivityWindow { get; set; }
        public Activity? CurrentActivity { get; set; }
        private IBurnedCaloriesService service;
        public MainViewModel(IBurnedCaloriesService service)
        {
            this.service = service;
            SelectedSport = "Running";
            BtnText = "Start";
            DeleteActivityCommand = new(OnDeleteActivity, CanDeleteActivity);
            StartStopTrackingCommand = new(OnStartStopTracking);
        }

        private void OnStartStopTracking()
        {
            if (BtnText == "Start")
            {
                TrackingText = "Tracking ...";
                BtnText = "Stop";
                ActivityWindow = new();
                CurrentActivity = new Activity
                {
                    Sport = SelectedSport,
                    Start = DateTime.Now
                };
                ActivityWindow.LoadActivity(CurrentActivity);
                ActivityWindow.Show();
            }
            else
            {
                CurrentActivity!.Duration = DateTime.Now - CurrentActivity.Start;
                CurrentActivity.Energy = service.GetBurnedCalories(SelectedSport, CurrentActivity.Duration);
                Activities.Add(CurrentActivity!);
                ActivityWindow!.IsClosingAllowed = true;
                ActivityWindow?.Close();
                TrackingText = "";
                BtnText = "Start";
            }


        }

        private void OnDeleteActivity(Activity? activity)
        {
            Activities.Remove(activity!);
        }

        private bool CanDeleteActivity(Activity? activity)
        {
            // HAU: ℹ️ can be done in one statement
            if (activity is null)
            {
                return false;
            }
            return true;
        }
    }
}
