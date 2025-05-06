namespace API_DelProyecto.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Marca { get; set; }
        public string Año { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
    }
}