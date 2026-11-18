using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMoreBooks.Models;

[Table("Books")]
public class Book
{
    [Key]
    [Column("BookID")]
    public int BookNumber { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(13, MinimumLength = 10)]
    [Column(TypeName = "varchar(13)")]
    public string ISBN { get; set; } = null!;

    [Column(TypeName = "decimal(8,2)")]
    public decimal Price { get; set; }

    public int NumberOfPages { get; set; }

    [Column("PublishedYear")]
    public int YearPublished { get; set; }

    public bool IsInStock { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorNumber { get; set; }
    public Author Author { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}