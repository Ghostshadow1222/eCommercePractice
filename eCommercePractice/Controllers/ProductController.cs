using eCommercePractice.Data;
using eCommercePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommercePractice.Controllers;

public class ProductController : Controller
{
    private readonly ProductDBContext _context;
    
    private const int ProductsPerPage = 3;

    public ProductController(ProductDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        // Ensure page is at least 1
        if (page < 1)
        {
            page = 1;
        }

        // Get total count of products
        int totalProducts = await _context.Products.CountAsync();

        // Calculate total pages
        int totalPagesNeeded = (int)Math.Ceiling((double)totalProducts / ProductsPerPage);

        // Ensure page doesn't exceed total pages
        if (page > totalPagesNeeded && totalPagesNeeded > 0)
        {
            page = totalPagesNeeded;
        }

        // Calculate skip amount for pagination
        int skip = (page - 1) * ProductsPerPage;

        // Get products for current page
        List<Product> products = await _context.Products
            .OrderBy(p => p.Title)
            .Skip(skip)
            .Take(ProductsPerPage)
            .ToListAsync();

        // Create view model with pagination data
        ProductIndexViewModel productListViewModel = new()
        {
            Products = products,
            CurrentPage = page,
            TotalPages = totalPagesNeeded,
            TotalProducts = totalProducts,
            ProductsPerPage = ProductsPerPage
        };

        return View(productListViewModel);
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
    public async Task<IActionResult> Edit(int id)
    {
        Product? product = await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound(); // Return a 404 Not Found response if the product does not exist
        }

        return View(product); // Pass the product to the view for editing
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Update(product);          // Update the product in the context
            await _context.SaveChangesAsync(); 
            
            TempData["Message"] = $"{product.Title} was updated successfully!";
            
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        // If user tried to go to the delete page without going through our website
        if (id <= 0)
        {
            return BadRequest(); // Return a 400 Bad Request response if the id is invalid
        }

        Product? product = await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [ActionName("Delete")]
    [HttpPost]
    public async Task<IActionResult> DeleteConfrimed(int id)
    {
        Product? product = await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return RedirectToAction(nameof(Index));
        }

        _context.Remove(product);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"{product.Title} was deleted successfully!";

        return RedirectToAction(nameof(Index));
    }
}
