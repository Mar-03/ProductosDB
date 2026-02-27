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

        protected override async Task OnInitializedAsync()
        {
            // Cargar productos desde la base de datos
            ListaProductos = await ProductoService.ObtenerProductosAsync();
        }

        void ConfirmarDatos()
        {
            MostrarBotonTransaccion = true;
        }

        void RealizarTransaccion()
        {
            MostrarErrores = true;

            if (string.IsNullOrWhiteSpace(TipoPagoSeleccionado))
                return;

            if (TipoPagoSeleccionado == "tarjeta" &&
                string.IsNullOrWhiteSpace(NumeroTarjeta))
                return;

            if (TipoPagoSeleccionado == "transferencia" &&
                string.IsNullOrWhiteSpace(NumeroTransferencia))
                return;

            Console.WriteLine("Transacción válida");
        }
    }
}