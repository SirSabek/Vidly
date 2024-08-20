using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}