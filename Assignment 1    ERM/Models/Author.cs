using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMoreBooks.Models;

[Table("Authors")]
public class Author
{
    [Key]
    [Column("AuthorID")]
    public int AuthorNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [MaxLength(1000)]
    public string? Biography { get; set; }

    [Column("BirthDate", TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public ICollection<Book> Books { get; set; } = new List<Book>();
}