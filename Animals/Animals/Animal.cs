using System.ComponentModel.DataAnnotations;

namespace Animals;

public class Animal
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; init; }

    [MaxLength(200)]
    public string? Description { get; init; }

    [Required]
    [MaxLength(200)]
    public required string Category { get; init; }

    [Required]
    [MaxLength(200)]
    public required string Area { get; init; }
}