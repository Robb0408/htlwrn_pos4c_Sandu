using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Shared.Models;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
namespace CashRegister.App.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<Product> products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        private ObservableCollection<ReceiptLineViewModel> basket = new ObservableCollection<ReceiptLineViewModel>();

        public ObservableCollection<ReceiptLineViewModel> Basket
        {
            get => basket;
            set => SetProperty(ref basket, value);
        }
    }
}
