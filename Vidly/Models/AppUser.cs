using Microsoft.AspNetCore.Identity;

namespace Vidly.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}