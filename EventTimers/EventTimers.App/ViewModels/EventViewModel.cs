using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace EventTimers.App;

[XmlType("Event")]
public partial class EventViewModel : ObservableObject
{
    private int id;
    public int Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }

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
        set
        {
            SetProperty(ref maxTime, value);
            SetProperty(ref timeLeft, value);
        }
    }

    private TimeSpan timeLeft;

    [JsonIgnore]
    [XmlIgnore]
    public TimeSpan TimeLeft
    {
        get => timeLeft;
        set => SetProperty(ref timeLeft, value);
    }

    private string? btnContent;

    [JsonIgnore]
    [XmlIgnore]
    public string BtnContent
    {
        get => btnContent ?? string.Empty;
        set => SetProperty(ref btnContent, value);
    }

    private double progress;

    [JsonIgnore]
    [XmlIgnore]
    public double Progress
    {
        get => progress;
        set => SetProperty(ref progress, value);
    }

    private bool isCloseToEnd;

    [JsonIgnore]
    [XmlIgnore]
    public bool IsCloseToEnd
    {
        get => isCloseToEnd;
        set => SetProperty(ref isCloseToEnd, value);
    }

    public EventViewModel()
    {
        BtnContent = "⏸";
        TimeLeft = MaxTime;
    }
}
