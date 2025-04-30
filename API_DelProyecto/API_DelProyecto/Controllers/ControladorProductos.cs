using Microsoft.AspNetCore.Mvc;
using ProyectoAutoPartes.Data;
using ProyectoAutoPartes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProyectoAutoPartes.Controllers
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

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productos = await _context.Productos.ToListAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, "Error interno del servidor al obtener productos");
            }
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }
                return Ok(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al obtener el producto");
            }
        }

        // Filtrar productos por criterios
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<Producto>>> FiltrarProductos(
            [FromQuery] string? nombre, 
            [FromQuery] string? marca, 
            [FromQuery] string? año, 
            [FromQuery] string? modelo)
        {
            try
            {
                var query = _context.Productos.AsQueryable();

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

                var productos = await query.ToListAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al filtrar productos");
                return StatusCode(500, "Error interno del servidor al filtrar productos");
            }
        }

        // Obtener marcas únicas
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<string>>> GetMarcas()
        {
            try
            {
                var marcas = await _context.Productos
                    .Select(p => p.Marca)
                    .Distinct()
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToListAsync();
                
                return Ok(marcas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener marcas");
                return StatusCode(500, "Error interno del servidor al obtener marcas");
            }
        }

        // Obtener años únicos
        [HttpGet("años")]
        public async Task<ActionResult<IEnumerable<string>>> GetAños()
        {
            try
            {
                var años = await _context.Productos
                    .Select(p => p.Año)
                    .Distinct()
                    .Where(a => !string.IsNullOrEmpty(a))
                    .ToListAsync();
                
                return Ok(años);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener años");
                return StatusCode(500, "Error interno del servidor al obtener años");
            }
        }

        // Obtener modelos únicos
        [HttpGet("modelos")]
        public async Task<ActionResult<IEnumerable<string>>> GetModelos()
        {
            try
            {
                var modelos = await _context.Productos
                    .Select(p => p.Modelo)
                    .Distinct()
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToListAsync();
                
                return Ok(modelos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener modelos");
                return StatusCode(500, "Error interno del servidor al obtener modelos");
            }
        }

        // Agregar un producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, "Error interno del servidor al crear el producto");
            }
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest("El ID del producto no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingProduct = await _context.Productos.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al actualizar el producto");
            }
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar producto con ID {id}");
                return StatusCode(500, "Error interno del servidor al eliminar el producto");
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}