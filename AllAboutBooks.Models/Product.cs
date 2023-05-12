using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutBooks.Models;

public class Product
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(1500)]
    public string Description { get; set; }

    [Required]
    [MaxLength(20)]
    [Display(Name = "ISBN")]
    public string InternationalStandardBookNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string Author { get; set; }

    [Required]
    [Display(Name = "List Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Required]
    [Display(Name = "Price for 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [Display(Name = "Price for 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [Display(Name = "Price for 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }

    [Display(Name = "Category")]
    public long CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public string ImageUrl { get; set; }
}
