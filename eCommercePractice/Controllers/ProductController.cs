using eCommercePractice.Data;
using eCommercePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommercePractice.Controllers;

public class ProductController : Controller
{
    private readonly ProductDBContext _context;

    public ProductController(ProductDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> allProducts = await _context.Products.ToListAsync(); // Retrieve all products from the database
        return View(allProducts);
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

            // TempData is used to pass data and will persist over a redirect
            TempData["Message"] = $"{p.Title} was created successfully!"; // Store a success message in TempData

            return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful creation
        }
        return View(p); // If the model state is invalid, return the same view with the product data to show validation errors
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Product? product = _context.Products.Where(p => p.ProductId == id).FirstOrDefault();

        if (product == null)
        {
            return NotFound(); // Return a 404 Not Found response if the product does not exist
        }

        return View(product); // Pass the product to the view for editing
    }
}
