using System.ComponentModel.DataAnnotations;

namespace Animals;

public class Animal
{
    public long Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; init; }
    
    [MaxLength(200)]
    public string? Description { get; init; }
    
    [Required]
    [MaxLength(200)]
    public string Category { get; init; }
    
    [Required]
    [MaxLength(200)]
    public string Area { get; init; }
}