using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrderImport.Database;

public class Order
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    [MaxLength(8)]
    [Precision(2)]
    public decimal OrderValue { get; set; }

    public Customer Customer { get; set; } = null!;
}