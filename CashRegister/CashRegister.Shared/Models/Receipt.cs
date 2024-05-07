using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CashRegister.Shared.Models
{
    public class Receipt
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("receiptTimestamp")]
        public DateTime ReceiptTimestamp { get; set; }

        [JsonPropertyName("receiptLines")]
        public List<ReceiptLine> ReceiptLines { get; set; } = [];

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
