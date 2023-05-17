using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllAboutBooks.Models;

public class ShoppingCart
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    public string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }

    [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public int Count { get; set; }

    [NotMapped]
    public double Price { get; set; }
}
