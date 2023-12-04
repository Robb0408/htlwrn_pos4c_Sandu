using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrderImport.Database;

[Index(nameof(Name), IsUnique=true)]
public class Customer
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    [MaxLength(8)]
    [Precision(2)]
    public decimal CreditLimit { get; set; }

    public List<Order> Orders { get; set; } = null!;
}