
using Microsoft.Data.SqlClient;
using ProductosDb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductoService
{
    private readonly string _connectionString;

    public ProductoService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<ProductoModel>> ObtenerProductosAsync()
    {
        var productos = new List<ProductoModel>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdProducto, Nombre, Descripcion, Stock, Precio FROM Producto";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    productos.Add(new ProductoModel
                    {
                        IdProducto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        Stock = reader.GetInt32(3),
                        Precio = reader.GetDecimal(4)
                    });
                }
            }
        }

        return productos;
    }
    public async Task<bool> ClienteExisteAsync(int nit)
{
    using SqlConnection conn = new SqlConnection(_connectionString);
    await conn.OpenAsync();

    string query = "SELECT COUNT(*) FROM Cliente WHERE NIT = @NIT";

    using SqlCommand cmd = new SqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@NIT", nit);

    int count = (int)await cmd.ExecuteScalarAsync();
    return count > 0;
}
    public async Task<string> RegistrarPedidoAsync(
    int nit,
    int idProducto,
    int cantidad,
    int idMetodo)
{
    using SqlConnection conn = new SqlConnection(_connectionString);
    await conn.OpenAsync();

    using SqlCommand cmd = new SqlCommand("sp_RegistroPedido", conn);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue("@NIT", nit);
    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
    cmd.Parameters.AddWithValue("@IdMetodo", idMetodo);

    var mensajeParam = new SqlParameter("@Mensaje",
        System.Data.SqlDbType.VarChar, 200)
    {
        Direction = System.Data.ParameterDirection.Output
    };

    cmd.Parameters.Add(mensajeParam);

    await cmd.ExecuteNonQueryAsync();

    return mensajeParam.Value?.ToString() ?? "";
}
}