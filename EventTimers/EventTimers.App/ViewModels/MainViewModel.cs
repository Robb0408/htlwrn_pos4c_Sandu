using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace EventTimers.App.ViewModels;

public class MainViewModel : ObservableObject
{
    private ObservableCollection<EventViewModel> events = [];

    public ObservableCollection<EventViewModel> Events
    {
        get => events;
        set => SetProperty(ref events, value);
    }

    private bool isEventsEmpty;

    public bool IsEventsEmpty
    {
        get => !isEventsEmpty;
        set => SetProperty(ref isEventsEmpty, value);
    }

    private readonly DispatcherTimer timer;

    private readonly List<EventViewModel> pausedEvents = [];

    public RelayCommand<int?> DeleteCommand { get; }
    public RelayCommand<int?> PauseResumeCommand { get; }
    public RelayCommand ClearTimersCommand { get; }
    public RelayCommand ExitAppCommand { get; }
    public RelayCommand LoadTimersCommand { get; }
    public RelayCommand SaveTimersCommand { get; }
    public RelayCommand CreateTimerCommand { get; set; }
    public MainViewModel()
    {
        isEventsEmpty = true;
        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += Timer_Tick;
        timer.Start();
        DeleteCommand = new(DeleteTimer);
        PauseResumeCommand = new(PauseResumeTimer);
        ClearTimersCommand = new(ClearAllTimers);
        ExitAppCommand = new(ExitApp);
        LoadTimersCommand = new(LoadTimersAsync);
        SaveTimersCommand = new(SaveTimers);
        CreateTimerCommand = new(CreateTimer);
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (Events.Count == 0)
        {
            IsEventsEmpty = true;
            return;
        }
        IsEventsEmpty = false;
        
        foreach (var item in Events.Except(pausedEvents))
        {
            if (item.TimeLeft.TotalSeconds > 0)
            {
                item.TimeLeft = item.TimeLeft.Subtract(TimeSpan.FromSeconds(1));
                item.Progress = item.TimeLeft.TotalSeconds / item.MaxTime.TotalSeconds * 100;
                item.IsCloseToEnd = item.TimeLeft.TotalSeconds <= 10;
            }
            else
            {
                timer.Stop();
                MessageBox.Show($"Timer '{item.Id}' has ended!", "Timer Ended", MessageBoxButton.OK, MessageBoxImage.Information);
                pausedEvents.Add(item);
                timer.Start();
            }
        }
        Events = new ObservableCollection<EventViewModel>(Events.OrderByDescending(x => x.TimeLeft.TotalSeconds > 0).ThenBy(x => x.TimeLeft));
    }
    private void DeleteTimer(int? id)
    {
        if (id is not null)
        {
            timer.Stop();
            var result = MessageBox.Show($"Do you really want to delete this timer?",
                "Deletion Confirmation",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning,
                MessageBoxResult.OK);
            if (result == MessageBoxResult.OK)
            {
                Events.Remove(Events.First(x => x.Id == id));
            }
            timer.Start();
        }
    }

    private void PauseResumeTimer(int? id)
    {
        if (id is not null)
        {
            var item = Events.First(x => x.Id == id);
            if (!pausedEvents.Contains(item))
            {
                item.BtnContent = "▶";
                pausedEvents.Add(item);
            }
            else
            {
                item.BtnContent = "⏸";
                pausedEvents.Remove(item);
            }
        }
    }

    private void ClearAllTimers()
    {
        var result = MessageBox.Show("Do you really want to delete all timers?",
            "Deletion Confirmation",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Warning,
            MessageBoxResult.OK);
        if (result == MessageBoxResult.OK)
        {
            Events.Clear();
        }
    }

    private void ExitApp()
    {
        var result = MessageBox.Show("Do you really want to exit?",
            "Exit Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question,
            MessageBoxResult.Yes);
        if (result == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    private void LoadTimersAsync()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Event Timers (*.json)|*.json|Event Timers (*.xml)|*.xml"
        };
        if (dialog.ShowDialog() == true)
        {
            var filePath = dialog.FileName;
            var extension = Path.GetExtension(filePath);
            if (extension == ".json")
            {
                async Task<string> json()
                {
                    var text = await File.ReadAllTextAsync(filePath);
                    return text;
                }
                json().ContinueWith(x => Events = JsonSerializer.Deserialize<ObservableCollection<EventViewModel>>(x.Result)!);
            }
            else if (extension == ".xml")
            {
                async Task<string> xml()
                {
                    var text = await File.ReadAllTextAsync(filePath);
                    return text;
                }
                xml().ContinueWith(x =>
                {
                    var serializer = new XmlSerializer(Events.GetType());
                    using var reader = new StringReader(x.Result);
                    Events = (ObservableCollection<EventViewModel>)serializer.Deserialize(reader)!;
                });
            }
        }
    }

    private void SaveTimers()
    {
        var dialog = new SaveFileDialog
        {
            Filter = "Event Timers (*.json)|*.json|Event Timers (*.xml)|*.xml"
        };
        if (dialog.ShowDialog() == true)
        {
            var filePath = dialog.FileName;
            var extension = Path.GetExtension(filePath);
            if (extension == ".json")
            {
                var json = JsonSerializer.Serialize(Events);
                async Task write() 
                { 
                    await File.WriteAllTextAsync(filePath, json); 
                };
                _ = write();
            }
            else if (extension == ".xml")
            {
                var serializer = new XmlSerializer(Events.GetType());
                using var writer = new StreamWriter(filePath);
                serializer.Serialize(writer, Events);
            }
        }
    }

    private void CreateTimer()
    {
        var viewModel = new CreateTimerViewModel();
        CreateTimerWindow window = new()
        {
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Title = "Create Timer",
            DataContext = viewModel
        };

        if (window.ShowDialog() == true)
        {
            Events.Add(new EventViewModel { Id = GetNextId(), Description = viewModel.Description, MaxTime = viewModel.MaxTime });
        }
    }
    private int GetNextId()
    {
        return Events.Count > 0 ? Events.Max(x => x.Id) + 1 : 0;
    }
}
