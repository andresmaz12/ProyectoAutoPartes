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
    public class CotizacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CotizacionesController> _logger;

        public CotizacionesController(ApplicationDbContext context, ILogger<CotizacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Cotizaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotizacion>>> GetCotizaciones()
        {
            try
            {
                return await _context.Cotizaciones
                    .Include(c => c.Producto)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las cotizaciones");
                return StatusCode(500, "Error interno del servidor al obtener cotizaciones");
            }
        }

        // POST: api/Cotizaciones
        [HttpPost]
        public async Task<ActionResult<Cotizacion>> CrearCotizacion(Cotizacion cotizacion)
        {
            try
            {
                // Verificar que el producto existe
                var producto = await _context.Productos.FindAsync(cotizacion.ProductoId);
                if (producto == null)
                {
                    return BadRequest("El producto especificado no existe");
                }

                // Verificar que hay suficiente stock
                if (producto.Stock < cotizacion.Cantidad)
                {
                    return BadRequest("No hay suficiente stock disponible");
                }

                // Establecer la fecha de solicitud
                cotizacion.FechaSolicitud = DateTime.Now;
                cotizacion.Atendida = false;

                _context.Cotizaciones.Add(cotizacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCotizacion", new { id = cotizacion.Id }, cotizacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cotización");
                return StatusCode(500, "Error interno del servidor al crear la cotización");
            }
        }

        // GET: api/Cotizaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cotizacion>> GetCotizacion(int id)
        {
            try
            {
                var cotizacion = await _context.Cotizaciones
                    .Include(c => c.Producto)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cotizacion == null)
                {
                    return NotFound();
                }

                return cotizacion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener cotización con ID {id}");
                return StatusCode(500, "Error interno del servidor al obtener la cotización");
            }
        }
        
        // PUT: api/Cotizaciones/5/atender
        [HttpPut("{id}/atender")]
        public async Task<IActionResult> AtenderCotizacion(int id)
        {
            try
            {
                var cotizacion = await _context.Cotizaciones.FindAsync(id);
                if (cotizacion == null)
                {
                    return NotFound();
                }

                cotizacion.Atendida = true;
                _context.Entry(cotizacion).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al atender cotización con ID {id}");
                return StatusCode(500, "Error interno del servidor al atender la cotización");
            }
        }
    }
}