using Microsoft.AspNetCore.Components;
using ProductosDb.Models;


namespace ProductosDb.Components
{
    public partial class Interfaz
    {
        [Inject]
        public ProductoService ProductoService { get; set; }

        PedidoModel pedido = new();
        List<ProductoModel> ListaProductos = new();

        string TipoPagoSeleccionado = "";
        string NumeroTarjeta = "";
        string NumeroTransferencia = "";

        bool MostrarErrores = false;
        bool MostrarBotonTransaccion = false;
        decimal Subtotal = 0;
         decimal Envio = 0;
        decimal IVA = 0;    
        decimal Total = 0;

        string MensajeResultado = "";

        protected override async Task OnInitializedAsync()
        {
            // Cargar productos desde la base de datos
            ListaProductos = await ProductoService.ObtenerProductosAsync();
        }

        async Task ConfirmarDatos()
        
{
    MostrarErrores = true;
    MensajeResultado = "";

    //  Validar cliente
    var clienteExiste = await ProductoService.ClienteExisteAsync(pedido.NIT);

    if (!clienteExiste)
    {
        MensajeResultado = "El cliente no existe.";
        return;
    }

    // Validar producto seleccionado
    var producto = ListaProductos
        .FirstOrDefault(p => p.IdProducto == pedido.IdProducto);

    if (producto == null)
    {
        MensajeResultado = "Seleccione un producto.";
        return;
    }

    if (pedido.Cantidad <= 0)
    {
        MensajeResultado = "Ingrese una cantidad válida.";
        return;
    }

    // Calcular subtotal
    Subtotal = producto.Precio * pedido.Cantidad;

    //  Calcular envío
    if (pedido.MetodoEntrega == "Tienda")
        Envio = 0;
    else
        Envio = 35;

    IVA = 0; 
    Total = Subtotal + Envio;

    MostrarBotonTransaccion = true;
}

       async Task RealizarTransaccion()
{
    MostrarErrores = true;
    MensajeResultado = "";

    if (string.IsNullOrWhiteSpace(TipoPagoSeleccionado))
    {
        MensajeResultado = "Seleccione método de pago";
        return;
    }

    int idMetodo = 1;

    if (pedido.MetodoEntrega == "Domicilio")
        idMetodo = 1;
    else if (pedido.MetodoEntrega == "Tienda")
        idMetodo = 2;

    var mensaje = await ProductoService.RegistrarPedidoAsync(
        pedido.NIT,
        pedido.IdProducto,
        pedido.Cantidad,
        idMetodo
    );

    MensajeResultado = mensaje;
}
    }
}