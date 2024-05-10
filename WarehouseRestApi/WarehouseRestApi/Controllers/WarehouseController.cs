using Microsoft.AspNetCore.Mvc;
using WarehouseRestApi.Repos;
using WarehouseRestApi.DTOs;

namespace WarehouseRestApi.Controllers;

[Route("/warehouse")]
[ApiController]
public class WarehouseController(IWarehouseRepo warehouseRepo) : ControllerBase
{
    private IWarehouseRepo _warehouseRepository = warehouseRepo;

    [HttpPost]
    public async Task<IActionResult> FulfillOrderAsync([FromBody] Order order)
    {
        var idWarehouse = await _warehouseRepository.GetWarehouseById(order.IdWarehouse);
        Console.WriteLine($"IdWarehouse: {idWarehouse}");
        if (idWarehouse is null) return NotFound($"{order.IdWarehouse} doesnt exist");
        
        var productPrice = await _warehouseRepository.GetPriceOfProductById(order.IdProduct);
        if (productPrice is null) return NotFound($"{order.IdProduct} doesnt exist");

        if (order.Amount <= 0) return BadRequest("OrderAmmount < 0!!!");

        var idOrder = await _warehouseRepository.GetMatchingOrderId(order.IdProduct,
            order.Amount,
            order.CreatedAt);
        if (idOrder is null) return NotFound("No match");

        if (await _warehouseRepository.IsOrderFulfilled(idOrder.Value))
        {
            return Conflict("Matching order is already fulfilled.");
        }

        var id = await _warehouseRepository.FulfillOrder(
            order.IdProduct,
            order.IdWarehouse,
            (int)idOrder,
            order.Amount,
            (decimal)productPrice,
            order.CreatedAt
        );

        return Ok($"{id}");
    }
    
    [HttpPost("/warehouseProcedure")]
    public async Task<IActionResult> FulfillOrderProcAsync([FromBody] Order order)
    {
        int id;
        try
        {
            id = await _warehouseRepository.FulfillOrderProcedure(
                order.IdProduct,
                order.IdWarehouse,
                order.Amount,
                order.CreatedAt
            );
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok($"{id}");
    }
}