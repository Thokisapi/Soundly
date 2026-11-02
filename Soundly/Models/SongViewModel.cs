using System.ComponentModel.DataAnnotations;
namespace Soundly.Models;

public class SongViewModel
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Artist { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Album { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateOnly ReleaseDate { get; set; }
}
