using System.ComponentModel.DataAnnotations;

namespace WarehouseRestApi.DTOs;

public record Order(
    [Required] int IdProduct,
    [Required] int IdWarehouse,
    [Required] int Amount,
    [Required] DateTime CreatedAt
);
