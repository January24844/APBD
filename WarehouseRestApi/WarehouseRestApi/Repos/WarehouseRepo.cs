using System.Data.SqlClient;
using System.Data;

namespace WarehouseRestApi.Repos;

public class WarehouseRepo(IConfiguration configuration) : IWarehouseRepo
{
    private IConfiguration _configuration = configuration;

    public async Task<int> FulfillOrder(int idProduct, int idWarehouse, int idOrder, int amount,
        decimal productPrice, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration["DefaultConnection"]);
        await connection.OpenAsync();
        var transaction = await connection.BeginTransactionAsync();
        try
        {
            await using var updateCommand = new SqlCommand();
            updateCommand.CommandText = "UPDATE [Order] SET FulfilledAt = SYSDATETIME() WHERE IdOrder = @IdOrder;";
            updateCommand.Parameters.AddWithValue("@IdOrder", idOrder);
            updateCommand.Connection = connection;
            updateCommand.Transaction = (SqlTransaction)transaction;
            await updateCommand.ExecuteNonQueryAsync();

            await using var insertCmd = new SqlCommand();
            insertCmd.CommandText =
                "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                "VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, SYSDATETIME()); " +
                "SELECT CONVERT(INT, SCOPE_IDENTITY());";
            insertCmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            insertCmd.Parameters.AddWithValue("@IdProduct", idProduct);
            insertCmd.Parameters.AddWithValue("@IdOrder", idOrder);
            insertCmd.Parameters.AddWithValue("@Amount", amount);
            insertCmd.Parameters.AddWithValue("@Price", productPrice * amount);
            insertCmd.Connection = connection;
            insertCmd.Transaction = (SqlTransaction)transaction;
            var prodWareId = (int?)await insertCmd.ExecuteScalarAsync();
            await transaction.CommitAsync();
            return (int)prodWareId!;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public async Task<int> FulfillOrderProcedure(int idProduct, int idWarehouse, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration["defaultconnection"]);
        await connection.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "AddProductToWarehouse";
        cmd.Connection = connection;
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
        var prodWareId = (int)(await cmd.ExecuteScalarAsync())!;
        return prodWareId;
    }
    
    public async Task<decimal?> GetPriceOfProductById(int idProduct)
    {
        await using var connection = new SqlConnection(_configuration["defaultconnection"]);
        await connection.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.CommandText = "SELECT p.Price FROM Product p WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Connection = connection;
        return (decimal?)await cmd.ExecuteScalarAsync();
    }

    public async Task<int?> GetWarehouseById(int idWarehouse)
    {
        await using var connection = new SqlConnection(_configuration["defaultconnection"]);
        await connection.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.CommandText = "SELECT IdWarehouse FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        cmd.Connection = connection;
        return (int?)await cmd.ExecuteScalarAsync();
    }

    public async Task<int?> GetMatchingOrderId(int idProduct, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration["defaultconnection"]);
        await connection.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.CommandText =
            "SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt;";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreatedAt", createdAt);
        cmd.Connection = connection;
        return (int?)await cmd.ExecuteScalarAsync();
    }

    public async Task<bool> IsOrderFulfilled(int idOrder)
    {
        await using var connection = new SqlConnection(_configuration["defaultconnection"]);
        await connection.OpenAsync();
        await using var cmd = new SqlCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder;";
        cmd.Parameters.AddWithValue("@IdOrder", idOrder);
        cmd.Connection = connection;

        var fulfilledIdOrderCount = (int?)await cmd.ExecuteScalarAsync();

        return fulfilledIdOrderCount != 0;
    }

    
    
    
}