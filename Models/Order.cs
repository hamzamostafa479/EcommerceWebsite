using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Models
{
  public class Order
  {
    public enum OrderStatus
    {
      Pending,
      Processing,
      Shipped,
      Delivered,
      Cancelled
    }


    public int OrderId { get; set; }


    public DateTime OrderDate { get; set; } = DateTime.Today;

    [Precision(18, 2)]
    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [StringLength(500)]
    public string? ShippingAddress { get; set; }

    [ValidateNever]
    public ApplicationUser User { get; set; }

    public string UserId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  }
}
