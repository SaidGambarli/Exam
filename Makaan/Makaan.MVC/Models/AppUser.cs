using Microsoft.AspNetCore.Identity;

namespace Makaan.MVC.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
