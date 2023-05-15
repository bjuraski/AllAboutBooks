using System.ComponentModel.DataAnnotations;

namespace AllAboutBooks.Models;

public class Company
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; }

    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}
