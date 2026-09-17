using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Models
{
  public class Category
  {
    public int CategoryId { get; set; }


    [StringLength(100)]
    public string Name { get; set; }



    [StringLength(500)]
    public string? Description { get; set; }



    public List<Product> Products { get; set; } = new();

  }
}