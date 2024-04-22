using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities;

public class Book
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(512)")]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(512)")]
    public string Author { get; set; } = string.Empty;

    public DateTime PublishDate { get; set; }
}
