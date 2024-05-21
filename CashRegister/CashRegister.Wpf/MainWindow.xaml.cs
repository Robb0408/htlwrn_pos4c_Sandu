using CashRegister.Shared.Dto;
using CashRegister.Shared.Models;
using CashRegister.App.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CashRegister.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<ReceiptLineViewModel> Basket { get; } = new();
        public decimal TotalSum => Basket.Sum(rl => rl.TotalPrice);

        // Add a HttpClient instance that we can use to access our backend Web API.
        // Note that this field is `static` because we only need a single instance
        // of the HTTP client for the running App instance.
        private static readonly HttpClient http = new()
        {
            BaseAddress = new Uri("http://localhost:5293/api/"),
            Timeout = TimeSpan.FromSeconds(5)
        };

        private readonly MainWindowViewModel viewModel;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            // Set the data context for data binding
            DataContext =  viewModel = new MainWindowViewModel();

            async Task LoadProducts()
            {
                var products = await http.GetFromJsonAsync<List<Product>>("products");
                if (products == null || products.Count == 0) return;
                foreach (var product in products) Products.Add(product);
            }
            Loaded += async (_, __) => await LoadProducts();
        }

        private void OnAddProduct(object sender, RoutedEventArgs e)
        {
            // Note that the buttons were generated from our products through data binding.
            // Therefore, we can acccess the bound product through the sender button's
            // `DataContext` property. We just need to do some type casting.
            if (((Button)sender).DataContext is not Product selectedProduct) return;

            // Lookup the product based on the ID
            var product = Products.First(p => p.ID == selectedProduct.ID);

            // New product -> add item to basket
            Basket.Add(new ReceiptLineViewModel
            {
                ProductID = product.ID,
                Amount = 1,
                ProductName = product.ProductName,
                TotalPrice = product.UnitPrice
            });

            // Inform UI that total sum has changed
            PropertyChanged?.Invoke(this, new(nameof(TotalSum)));
        }

        private async void OnCheckout(object sender, RoutedEventArgs e)
        {
            // Turn all items in the basket into DTO objects
            var dto = Basket.GroupBy(rl => rl.ProductID)
                .Select(g => new ReceiptLineDto
                {
                    ProductID = g.Key,
                    Amount = g.Count()
                });

            // Send the receipt to the backend
            var response = await http.PostAsJsonAsync("products/receipts", dto);

            // Throw exception if something went wrong
            response.EnsureSuccessStatusCode();

            // Clear basket so shopping can start from scratch
            Basket.Clear();

            // Inform UI that total sum has changed
            PropertyChanged?.Invoke(this, new(nameof(TotalSum)));
        }
    }
}