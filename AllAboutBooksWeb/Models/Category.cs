using System.ComponentModel.DataAnnotations;

namespace AllAboutBooksWeb.Models;

public class Category
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public long DisplayOrder { get; set; }
}
