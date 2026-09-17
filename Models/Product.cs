using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    [Required]
    [StringLength(200)]
    public string Name { get; set;  } = string.Empty;


    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [Precision(18, 2)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be Greater than 0")]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity cannot be negative")]
    public int StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; } = null!;

    [ValidateNever]
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

  }
}
