using PA1_Sandu.Logic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

// HAU: ℹ️ use better namespace structure e.g. OrderStatistics.App 
namespace PA1_Sandu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run (--orders) or (--customer [--asc or --desc])");
                Environment.Exit(1);
            }

            List<string[]> orderLines = new();

            try
            {
                orderLines = File.ReadLines("order-data.txt").Select(line => line.Split("\t")).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(2);
            }

            // HAU: ℹ️ remove not used variables
            var orderHeader = orderLines[0].Skip(1);
            var detailHeader = orderLines[1].Skip(1);

            List<Order> orders = new();

            Order? tempOrder = null;
            for (int i = 2; i < orderLines.Count; i++) 
            {
                if (orderLines[i].Contains("ORDER"))
                {
                    tempOrder = new Order
                    {
                        OrderId = Convert.ToInt32(orderLines[i][1]),
                        Customer = orderLines[i][2],
                        DeliverToCountry = orderLines[i][3],
                        Details = new List<Detail>()
                    };
                    orders.Add(tempOrder);
                }
                else
                {
                    tempOrder!.Details.Add(new Detail
                    {
                        Product = orderLines[i][1],
                        Amount = Convert.ToInt32(orderLines[i][2]),
                        UnitPriceEur = Convert.ToInt32(orderLines[i][3]),
                        PriceEur = Convert.ToInt32(orderLines[i][4])
                    });
                }
            }

            if (args[0] == "--orders" && !args.Contains("--asc") && !args.Contains("--desc"))
            {
                var result = Order.RevenuePerId(orders);
                foreach (var item in result)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }
            else if (args[0] == "--customers")
            {
                var result = Order.RevenuePerCustomer(orders, args.Length >= 2 ? args[1] : "");
                int totalRevenue = Order.GetTotalRevenue(orders);
                foreach (var item in result)
                {
                    
                    Console.Write($"{item.Key}: {item.Value}");
                    // HAU: ℹ️ small remark: code re-use
                    //         add an bool addPercentage parameter to RevenuePerCustomer method
                    //         and return e.g. a List<CustomerRevenue> with an Percentage Property
                    if (args.Contains("--percentage"))
                    {
                        var rate = (decimal)item.Value * 100 / totalRevenue;
                        Console.Write($" ({Math.Round(rate, 2)} %)");
                    }
                    Console.WriteLine();
                }
            }
            else if (args[0] == "--country")
            {
                var result = Order.RevenuePerCountry(orders);
                int totalRevenue = Order.GetTotalRevenue(orders);
                foreach (var item in result)
                {
                    var rate = (decimal)item.Value * 100 / totalRevenue;
                    Console.WriteLine($"{item.Key}: {item.Value} ({Math.Round(rate, 2)} %)");
                }
            }
        }
    }
}