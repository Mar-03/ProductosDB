
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
}