using Ecommerce_Project.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Controllers
{
  public class ProductController(ApplicationDbContext context, IValidator<Product> _validator) : Controller
  {

    private readonly ApplicationDbContext _context = context;
    public async Task<IActionResult> Index(int? categoryId, string? searchString)
    {
      var productsQuery = _context.Products
         .Include(p => p.Category)
         .Where(p => p.IsActive);

      if (categoryId.HasValue)
      {
        productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
      }

      if (!string.IsNullOrEmpty(searchString))
      {
        productsQuery = productsQuery.Where(p =>
            p.Name.Contains(searchString) ||
            (p.Description != null && p.Description.Contains(searchString)));
      }

      ViewBag.Categories = await _context.Categories.ToListAsync();
      ViewBag.CurrentCategory = categoryId;
      ViewBag.SearchString = searchString;

      return View(await productsQuery.ToListAsync());
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      var product = await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(m => m.ProductId == id);

      if (product == null || !product.IsActive)
      {
        return NotFound();
      }

      return View(product);

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.Categories = new SelectList(
           await _context.Categories.ToListAsync(),
           "CategoryId",
           "Name");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product prod)
    {
      var result = await _validator.ValidateAsync(prod);

      if (!result.IsValid)
      {
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
      }

      if (ModelState.IsValid)
      {
        _context.Products.Add(prod);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      ViewBag.Categories = new SelectList(
         await _context.Categories.ToListAsync(),
         "CategoryId",
         "Name",
         prod.CategoryId);
      return View(prod);

    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ValidateField([FromBody] ValidateFieldRequest request)
    {
      if (request == null)
      {
        return BadRequest(new { isValid = false, errors = new[] { "Invalid request" } });
      }

      var product = new Product
      {
        ProductId = request.ProductId,
        Name = request.Name ?? string.Empty,
        SKUNumber = request.SKUNumber,
        Price = request.Price,
        SalePrice = request.SalePrice
      };

      var result = await _validator.ValidateAsync(product, options =>
          options.IncludeProperties(request.FieldName));

      return Json(new
      {
        isValid = result.IsValid,
        errors = result.Errors.Select(e => e.ErrorMessage)
      });
    }

    public class ValidateFieldRequest
    {
      [System.Text.Json.Serialization.JsonPropertyName("fieldName")]
      public string FieldName { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("productId")]
      public int ProductId { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("name")]
      public string Name { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("skuNumber")]
      public string SKUNumber { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("price")]
      public decimal? Price { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("salePrice")]
      public decimal? SalePrice { get; set; }
    }

  }
}