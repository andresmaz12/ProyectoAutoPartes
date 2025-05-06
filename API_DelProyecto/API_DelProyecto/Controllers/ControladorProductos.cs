using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_DelProyecto.Data;
using API_DelProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DelProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ApplicationDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                return await _context.Productos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
                return StatusCode(500, "Error interno del servidor al obtener productos");
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return NotFound();
                }

                return producto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al obtener el producto");
            }
        }

        // GET: api/Productos/marcas
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<string>>> GetMarcas()
        {
            try
            {
                return await _context.Productos
                    .Select(p => p.Marca)
                    .Distinct()
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener marcas");
                return StatusCode(500, "Error interno del servidor al obtener marcas");
            }
        }

        // GET: api/Productos/años
        [HttpGet("años")]
        public async Task<ActionResult<IEnumerable<string>>> GetAños()
        {
            try
            {
                return await _context.Productos
                    .Select(p => p.Año)
                    .Distinct()
                    .Where(a => !string.IsNullOrEmpty(a))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener años");
                return StatusCode(500, "Error interno del servidor al obtener años");
            }
        }

        // GET: api/Productos/modelos
        [HttpGet("modelos")]
        public async Task<ActionResult<IEnumerable<string>>> GetModelos()
        {
            try
            {
                return await _context.Productos
                    .Select(p => p.Modelo)
                    .Distinct()
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener modelos");
                return StatusCode(500, "Error interno del servidor al obtener modelos");
            }
        }

        // GET: api/Productos/filtrar
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<Producto>>> FiltrarProductos(
            [FromQuery] string nombre = null,
            [FromQuery] string marca = null,
            [FromQuery] string año = null,
            [FromQuery] string modelo = null)
        {
            try
            {
                var query = _context.Productos.AsQueryable();

                // Aplicar filtros si existen
                if (!string.IsNullOrEmpty(nombre))
                {
                    query = query.Where(p => p.Nombre.Contains(nombre));
                }

                if (!string.IsNullOrEmpty(marca))
                {
                    query = query.Where(p => p.Marca == marca);
                }

                if (!string.IsNullOrEmpty(año))
                {
                    query = query.Where(p => p.Año == año);
                }

                if (!string.IsNullOrEmpty(modelo))
                {
                    query = query.Where(p => p.Modelo == modelo);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al filtrar productos");
                return StatusCode(500, "Error interno del servidor al filtrar productos");
            }
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un producto");
                return StatusCode(500, "Error interno del servidor al crear el producto");
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"Error de concurrencia al actualizar el producto con ID {id}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al actualizar el producto");
            }

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al eliminar el producto");
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}