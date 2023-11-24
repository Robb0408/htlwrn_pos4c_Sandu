using System.Reflection.Metadata.Ecma335;

namespace PA1_Sandu.Logic
{
    public class Order
    {
        public int OrderId { get; set; }
        public required string Customer { get; set; }
        public required string DeliverToCountry { get; set; }
        public required List<Detail> Details { get; set; }

        // HAU: ℹ️ move these methods to an e.g. "RevenueCalculator" class 
        //         logically these method belong to all orders not only to a single instance
        public static IEnumerable<KeyValuePair<int, int>> RevenuePerId(List<Order> orders)
        {
            return orders
                .ToDictionary(order => order.OrderId, order => order.Details
                    .Sum(detail => detail.PriceEur))
                .OrderByDescending(order => order.Value);
        }

        public static IEnumerable<KeyValuePair<string, int>> RevenuePerCustomer(List<Order> orders, string asc)
        {
            

            var temp = orders
                .GroupBy(order => order.Customer)
                .ToDictionary(group => group.Key, group => group.Sum(o => o.Details
                .Sum(detail => detail.PriceEur)));

            // HAU: ✅ good job for splitting the linq statement and re-use code above
            //          small remark: use a bool property for asc 
            return asc == "--asc" ? 
                temp.OrderBy(group => group.Value) : 
                temp.OrderByDescending(group => group.Value);
                
        }

        public static IEnumerable<KeyValuePair<string, int>> RevenuePerCountry(List<Order> orders)
        {
            return orders
                .GroupBy(order => order.DeliverToCountry)
                .ToDictionary(group => group.Key, group => group
                    .Sum(g => g.Details.Sum(d => d.PriceEur)))
                .OrderByDescending(group => group.Value);
        }
        
        public static int GetTotalRevenue(List<Order> orders)
        {
            return orders.Sum(order => order.Details.Sum(detail => detail.PriceEur));
        }
    }
}