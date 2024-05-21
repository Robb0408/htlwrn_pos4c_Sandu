using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTimers.App.ViewModels
{
    public class CreateTimerViewModel : ObservableObject
    {
        private string? description;
        public string Description
        {
            get => description ?? string.Empty;
            set => SetProperty(ref description, value);
        }

        private TimeSpan maxTime;
        public TimeSpan MaxTime
        {
            get => maxTime;
            set => SetProperty(ref maxTime, value);
        }
    }
}
