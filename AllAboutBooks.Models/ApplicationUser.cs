using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AllAboutBooks.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string StreetAddress { get; set; }

    public string PostalCode { get; set; }
}
