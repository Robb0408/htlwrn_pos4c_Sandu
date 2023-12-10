using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrderImport.Database;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    
    [Column(TypeName = "decimal(8,2)")]
    public decimal OrderValue { get; set; }

    [Required]
    public Customer Customer { get; set; } = null!;
    public int CustomerId { get; set; }
}