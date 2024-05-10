namespace WarehouseRestApi.Repos;

public interface IWarehouseRepo
{
    Task<int> FulfillOrder(int idProduct, int idWarehouse, int idOrder, 
        int amount, decimal productPrice, DateTime createdAt);
    Task<int> FulfillOrderProcedure(int idProduct, int idWarehouse, int amount, DateTime createdAt);
    Task<decimal?> GetPriceOfProductById(int idProduct);
    Task<int?> GetWarehouseById(int idWarehouse);
    Task<int?> GetMatchingOrderId(int idProduct, int amount, DateTime createdAt);
    Task<bool> IsOrderFulfilled(int idOrder);
}