using CashRegister.Shared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashRegister.App.ViewModels
{
    public class ReceiptLineViewModel
    {
        public class ReceiptLineViewModel : ObservableObject
        {
            private int productId;
            public int ProductID
            {
                get => productId;
                set => SetProperty(ref productId, value);
            }

            private string? productName;
            public string ProductName
            {
                get => productName ?? string.Empty;
                set => SetProperty(ref productName, value);
            }

            private int amount;
            public int Amount
            {
                get => amount;
                set => SetProperty(ref amount, value);
            }

            private decimal totalPrice;
            public decimal TotalPrice
            {
                get => totalPrice;
                set => SetProperty(ref totalPrice, value);
            }
        }
    }
}
