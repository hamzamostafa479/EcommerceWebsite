using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ecommerce_Project.Models
{
  public class Order
  {
    public int OrderId { get; set; }


    public DateTime OrderDate { get; set; } = DateTime.Today;


    public decimal TotalAmount { get; set; }


    [ValidateNever]
    public ApplicationUser User { get; set; }

    public string UserId { get; set; }
  }
}
