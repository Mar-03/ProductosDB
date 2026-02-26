using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProductosDb.Models
{
    public class PedidoModel
    {
       public int NIT { get; set; }
       public int IdProducto { get; set; }
       public int Cantidad { get; set; }
       public int IdMetodo { get; set; }
    }
}