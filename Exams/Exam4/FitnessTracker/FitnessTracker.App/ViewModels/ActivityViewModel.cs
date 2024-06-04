using CommunityToolkit.Mvvm.ComponentModel;
using FitnessTracker.App.Models;
using FitnessTracker.App.Services;
using System;
// HAU: ℹ️ remove not used usings
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

// HAU: ℹ️ use file scoped namespace (single line) declaration to reduce code nesting
namespace FitnessTracker.App.ViewModels
{
    public class ActivityViewModel : ObservableObject
    {
        private string? sport;
        public string Sport
        {
            get => sport ?? string.Empty;
            set => SetProperty(ref sport, value);
        }

        private DateTime start;
        public DateTime Start
        {
            get => start;
            set => SetProperty(ref start, value);
        }

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        private double energy;
        public double Energy
        {
            get => double.Round(energy, 2);
            set => SetProperty(ref energy, value);
        }

        public void LoadActivity(Activity activity)
        {
            Sport = activity.Sport;
            Start = activity.Start;
            Duration = activity.Duration;
            Energy = activity.Energy;
        }

        // HAU: ℹ️ can be made readonly
        private DispatcherTimer timer;

        // HAU: 💥 fields should be private, start with a lowercase letter and use camelCase (use a property here)
        public IBurnedCaloriesService service;

        public ActivityViewModel(IBurnedCaloriesService service)
        {
            timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            this.service = service;
        }

        // HAU: ⚠️ add a destructor ~ActivityViewModel to stop the timer

        // HAU: ⚠️ use better method name like OnTimerTick avoid underscores in method names
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // HAU: ⚠️ recalculate the duration by subtracting the start time from the current time
            Duration += TimeSpan.FromSeconds(1);
            // HAU: ⚠️ logic bug: += add the new calculated burned calories to the current burned energy value - use = instead
            Energy += service.GetBurnedCalories(Sport, Duration);
        }
    }
}
