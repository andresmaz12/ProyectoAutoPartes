using System;

namespace ProyectoAutoPartes
{
    // Clase para almacenar los datos de una venta temporal
    public class VentaTemporal
    {
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NitCliente { get; set; }
        public int Cantidad { get; set; }
        public string NoFactura { get; set; }
        public DateTime FechaVenta { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioTotal { get; set; }

        public VentaTemporal(string idProducto, string nombreProducto, string nitCliente, int cantidad, string noFactura, DateTime fechaVenta, double precioUnitario, double precioTotal)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            NitCliente = nitCliente;
            Cantidad = cantidad;
            NoFactura = noFactura;
            FechaVenta = fechaVenta;
            PrecioUnitario = precioUnitario;
            PrecioTotal = precioTotal;
        }
    }
}