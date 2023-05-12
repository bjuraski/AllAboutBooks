using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllAboutBooks.Models;

public class Category
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    [DisplayName("Category Name")]
    public string Name { get; set; }

    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
    public long DisplayOrder { get; set; }
}
