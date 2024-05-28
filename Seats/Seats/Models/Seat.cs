using System.ComponentModel;

namespace Seats.Models;

public class Seat : Space, INotifyPropertyChanged
{
    private bool occupied;
    public bool Occupied
    {
        get => occupied;
        set
        {
            occupied = value;
            OnPropertyChanged();
        }
    }

    public decimal Price => Row switch
    {
        >= 8 => 100,
        >= 5 => 75,
        <= 1 => 25,
        _ => 50
    };

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Occupied)));
    }
}