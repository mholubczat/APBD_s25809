using System.ComponentModel.DataAnnotations;

namespace Warehouse;

public class AddProductModel
{
    [Required]
    public required int ProductId { get; set; }

    [Required]
    public required int WarehouseId { get; set; }

    [Required]
    public required int Amount { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
}