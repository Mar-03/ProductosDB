using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TuProyecto.Models
{
    public class Pedido
    {
        [Required]
        public string NombreCliente {get; set;}

        [Required]
        public string Producto { get; set; }

        [Range(1,1000)]
        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}