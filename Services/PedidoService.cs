using Microsoft.Data.SqlClient;
using ProductosDb.Models;
using System.Data;

public class PedidoService
{
    private readonly string _connectionString;

    public PedidoService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string RegistrarPedido(PedidoModel pedido)
    {
        string mensaje = string.Empty;

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_RegistroPedido", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NIT", pedido.NIT);
            cmd.Parameters.AddWithValue("@IdProducto", pedido.IdProducto);
            cmd.Parameters.AddWithValue("@Cantidad", pedido.Cantidad);
            cmd.Parameters.AddWithValue("@IdMetodo", pedido.IdMetodo);

            SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar,200);
            mensajeParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(mensajeParam);

            conn.Open();
            cmd.ExecuteNonQuery();

            mensaje = mensajeParam.Value?.ToString() ?? "Sin respuesta";

        }
        return mensaje;
    }
    
}