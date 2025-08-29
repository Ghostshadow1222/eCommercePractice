using eCommercePractice.Models;

namespace eCommercePractice.Models;

/// <summary>
/// ViewModel for the Product Index page with pagination support
/// </summary>
public class ProductIndexViewModel
{
    /// <summary>
    /// The products to display on the current page
    /// </summary>
    public List<Product> Products { get; set; } = new();

    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Total number of products (after filtering)
    /// </summary>
    public int TotalProducts { get; set; }

    /// <summary>
    /// Number of products displayed per page
    /// </summary>
    public int ProductsPerPage { get; set; }

    /// <summary>
    /// Indicates if there is a previous page
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Indicates if there is a next page
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>
    /// Optional search term to filter by product title
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Optional minimum price filter
    /// </summary>
    public double? MinPrice { get; set; }

    /// <summary>
    /// Optional maximum price filter
    /// </summary>
    public double? MaxPrice { get; set; }
}