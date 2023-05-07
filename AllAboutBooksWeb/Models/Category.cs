using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllAboutBooksWeb.Models;

public class Category
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(200)]
    [DisplayName("Category Name")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Display Order")]
    public long DisplayOrder { get; set; }
}
