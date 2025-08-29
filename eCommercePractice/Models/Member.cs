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
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters (letters and numbers only).")]
    [StringLength(25, ErrorMessage = "Your username cannot be more than 25 characters")]
    public required string Username { get; set; }

    /// <summary>
    /// Email address of the member
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Member's password
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password must be between 6 and 50 characters")]
    public required string Password { get; set; }

    /// <summary>
    /// The date when the member was born
    /// </summary>
    public DateOnly DateOfBirth { get; set; }
}

public class RegistrationViewModel
{
    /// <summary>
    /// Public facing username defined by the member
    /// Alphanumeric characters only
    /// </summary>
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters (letters and numbers only).")]
    [StringLength(25, ErrorMessage = "Your username cannot be more than 25 characters")]
    public required string Username { get; set; }

    /// <summary>
    /// Email address of the member
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    /// <summary>
    /// Member's password
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Your password must be between 6 and 50 characters")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }

    /// <summary>
    /// The date when the member was born
    /// </summary>
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}

public class LoginViewModel
{
    public required string UsernameOrEmail { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
}