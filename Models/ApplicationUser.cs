using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Project.Models
{
  public class ApplicationUser : IdentityUser
  {
    public List<Order> Orders { get; set; } = new();
  }
}
