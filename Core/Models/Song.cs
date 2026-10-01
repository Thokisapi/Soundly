using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Song : IValidatableObject
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReleaseDate < new DateOnly(1888, 1, 1))
        {
            yield return new ValidationResult(
                "Songs must have a release date of 1 January 1888 or later.",
                new[] { nameof(ReleaseDate) });
        }
    }
}