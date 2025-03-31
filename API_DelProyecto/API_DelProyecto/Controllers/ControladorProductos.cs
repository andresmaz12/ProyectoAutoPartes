using Microsoft.AspNetCore.Mvc;
using ProyectoAutoPartes.Data;
using ProyectoAutoPartes.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProyectoAutoPartes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            return _context.Productos.ToList();
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        // Agregar un producto
        [HttpPost]
        public ActionResult<Producto> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public IActionResult PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
