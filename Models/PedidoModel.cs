using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProductosDb.Models
{
    public class PedidoModel
    {
        [Required]
        public int NIT { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public int IdMetodo { get; set; }  // 👈 AGREGA ESTO

        [Required]
        public string MetodoEntrega { get; set; } = string.Empty;

        [Required]
        public string TipoPedido { get; set; } = string.Empty;

        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Referencia { get; set; }
    }
}