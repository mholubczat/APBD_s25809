using System.ComponentModel.DataAnnotations;

namespace Warehouse;

public class AddProductModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ProductId has to be a positive number")]
    public required int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "WarehouseId has to be a positive number")]
    public required int WarehouseId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Amount has to be a positive number")]
    public required int Amount { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
}