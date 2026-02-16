using Microsoft.AspNetCore.Identity;

namespace VisitorLog.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}