using eCommercePractice.Data;
using eCommercePractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommercePractice.Controllers;

public class ProductController : Controller
{
    private readonly ProductDBContext _context;

    public ProductController(ProductDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(p);          // Add the new product to the context
            await _context.SaveChangesAsync(); // Save changes to the database

            return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful creation
        }
        return View(p); // If the model state is invalid, return the same view with the product data to show validation errors
    }
}
