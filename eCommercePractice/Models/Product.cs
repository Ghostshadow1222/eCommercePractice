using System.ComponentModel.DataAnnotations;

namespace eCommercePractice.Models;

/// <summary>
/// Represents an individual product on the site
/// </summary>
public class Product
{
    /// <summary>
    /// The unique identifier for the product
    /// </summary>
    [Key]
    public int ProductId { get; set; }

    /// <summary>
    /// The user facing title of the product
    /// </summary>
    [StringLength(50, ErrorMessage = "Titles cannot be more than 50 characters")]
    public required string Title { get; set; }

    /// <summary>
    /// The current sales price of the product
    /// </summary>
    [Range(0, 10_000, ErrorMessage = "Price must be between 0 and 10,000")]
    [DataType(DataType.Currency)]
    public double Price { get; set; }
}
