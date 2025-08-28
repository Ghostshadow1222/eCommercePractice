using System.ComponentModel.DataAnnotations;

namespace eCommercePractice.Models;

/// <summary>
/// Represents a singluar website user
/// </summary>
public class Member
{
    /// <summary>
    /// Unique identifier for the member
    /// </summary>
    [Key]
    public int MemberId { get; set; }

    /// <summary>
    /// Public facing username defined by the member
    /// Alphanumeric characters only
    /// </summary>

    public required string Username { get; set; }

    /// <summary>
    /// Email address of the member
    /// </summary>

    public required string Email { get; set; }

    /// <summary>
    /// Member's password
    /// </summary>

    public required string Password { get; set; }

    /// <summary>
    /// The date when the member was born
    /// </summary>

    public DateOnly DateOfBirth { get; set; }


}
