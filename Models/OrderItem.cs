using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Project.Models
{
  public class OrderItem
  {
    public int OrderItemId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }


    [Required]
    [Precision(18, 2)]
    public int UnitPrice { get; set; }


    public int OrderId { get; set; }
    public int ProductId { get; set; }

    [ValidateNever]
    public Order Order { get; set; } = null!;

    [ValidateNever]

    public Product Product { get; set; } = null!;

    [NotMapped]
    public decimal TotalPrice => Quantity * UnitPrice; 

  }
}