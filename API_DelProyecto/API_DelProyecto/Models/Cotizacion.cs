using System;

namespace ProyectoAutoPartes.Models
{
    public class Cotizacion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Cantidad { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool Atendida { get; set; }

        // Propiedad de navegación
        public Producto Producto { get; set; }
    }
}