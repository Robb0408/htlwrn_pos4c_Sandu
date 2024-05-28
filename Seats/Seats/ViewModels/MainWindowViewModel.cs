using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Seats.Models;

namespace Seats.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public List<List<Space>> Spaces { get; } = [];

    public RelayCommand<Seat> SeatClick { get; }

    private ObservableCollection<Seat> orderedSeats = [];
    public ObservableCollection<Seat> OrderedSeats
    {
        get => orderedSeats;
        set => SetProperty(ref orderedSeats, value);
    }

    public decimal TotalSum => OrderedSeats.Sum(seat => seat.Price);

    public RelayCommand<Seat> RemoveOrderedSeatCommand { get; }
    public RelayCommand<string> FindBestSeatCommand { get; }
    public MainWindowViewModel()
    {
        SeatClick = new(OnSeatClick);
        RemoveOrderedSeatCommand = new(OnRemoveOrderedSeat, CanRemoveOrderedSeat);
        FindBestSeatCommand = new(OnFindBestSeat, CanFindBestSeat);

        OrderedSeats.CollectionChanged += (sender, args) =>
        {
            OnPropertyChanged(nameof(TotalSum));
        };
        // Fill with data
        Spaces = BuildSpacesFromFloorplan();
        OccupyRandomSeats(Spaces, 20);
    }

    private bool CanFindBestSeat(string? textBoxContent)
    {
        if (int.TryParse(textBoxContent, out var amount))
        {
            return amount > 0 && amount < 5;
        }
        return false;
    }

    private void OnFindBestSeat(string? textBoxContent)
    {
        var amount = int.Parse(textBoxContent!);
        var bestSeats = SearchBestSeat(false, amount);
        if (bestSeats.Count == amount)
        {
            bestSeats.ForEach(seat => SeatClick.Execute(seat));
            return;
        }
        bestSeats = SearchBestSeat(true, amount);
        if (bestSeats.Count == amount)
        {
            bestSeats.ForEach(seat => SeatClick.Execute(seat));
            return;
        }
        MessageBox.Show("No suitables seats found.", "Too many seats occupied", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

    }

    private List<Seat> SearchBestSeat(bool withAisle, int amount)
    {
        var bestSeats = new List<Seat>();
        var maxCol = Spaces[0].Count;
        for (var row = Spaces.Count; row > 0; row--)
        {
            for (var col = 0; col < maxCol; col++)
            {
                if (bestSeats.Count == amount)
                {
                    break;
                }
                else if (Spaces[row - 1][col] is Aisle aisle && withAisle)
                {
                    continue;
                }
                else if (Spaces[row - 1][col] is Seat seat && !seat.Occupied)
                {
                    bestSeats.Add(seat);
                }
                else
                {
                    bestSeats.Clear();
                }
            }
            if (bestSeats.Count != amount)
            {
                bestSeats.Clear();
            }
        }
        return bestSeats;
    }
    private void OnRemoveOrderedSeat(Seat? seat)
    {
        if (seat is null)
        {
            return;
        }
        seat.Occupied = false;
        seat.OnPropertyChanged();
        OrderedSeats.Remove(seat);
    }

    private bool CanClickSeat(Seat? seat)
    {
        if (seat is null && seat!.Occupied)
        {
            return false;
        }
        return true;
    }

    private void OnSeatClick(Seat? seat)
    {
        seat.Occupied = true;
        seat.OnPropertyChanged();
        OrderedSeats.Add(seat);
    }

    private bool CanRemoveOrderedSeat(Seat? seat)
    {
        if (seat is null)
        {
            return false;
        }
        return true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    #region DO NOT MODIFY

    // The following method fills the floorplan with seats, aisles, and wheelchair spaces.
    // DO NOT MODIFY THIS METHOD. It is ok as it is.
    public static List<List<Space>> BuildSpacesFromFloorplan()
    {
        const int numberOfRows = 10;
        const int numberOfColumns = 15;
        const int aisleWidth = 2;
        const int seatsFromAisleToWall = 2;
        const int rowWithWheelchairSpace = 5;
        const int aisleRow = 4;

        var spaces = new List<List<Space>>();
        for (var row = 0; row < numberOfRows; row++)
        {
            var rowSpaces = new List<Space>();
            for (var col = 0; col < numberOfColumns; col++)
            {
                if (row + 1 == aisleRow)
                {
                    rowSpaces.Add(new Aisle { Row = row, Column = col });
                }
                else if (col + 1 is > seatsFromAisleToWall and <= seatsFromAisleToWall + aisleWidth
                    || col + 1 is > numberOfColumns - seatsFromAisleToWall - aisleWidth and <= numberOfColumns - seatsFromAisleToWall)
                {
                    rowSpaces.Add(new Aisle { Row = row, Column = col });
                }
                else
                {
                    if (col + 1 is <= seatsFromAisleToWall or > numberOfColumns - seatsFromAisleToWall && row + 1 == rowWithWheelchairSpace)
                    {
                        rowSpaces.Add(new WheelchairSpace { Row = row, Column = col });
                    }
                    else
                    {
                        rowSpaces.Add(new Seat { Row = row, Column = col });
                    }
                }
            }

            spaces.Add(rowSpaces);
        }

        return spaces;
    }

    // The following method randomly occupies a given number of seats.
    // DO NOT MODIFY THIS METHOD. It is ok as it is.
    public static void OccupyRandomSeats(List<List<Space>> spaces, int numberOfSeatsToOccupy)
    {
        var random = new Random(11);
        var seats = spaces.SelectMany(row => row.OfType<Seat>()).ToList();
        for (var i = 0; i < numberOfSeatsToOccupy; i++)
        {
            var seat = seats[random.Next(seats.Count)];
            seat.Occupied = true;
            seats.Remove(seat);
        }
    }

    // NOTE: PLEASE DO NOT use regions in your code - they are code smells!
    #endregion
}
