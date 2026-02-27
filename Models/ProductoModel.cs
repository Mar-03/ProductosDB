namespace ProductosDb.Models
{
   
public class ProductoModel
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
     public int NIT { get; set; }
    public string NombreCompleto { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
}}