using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAutoPartes.Data;
using ProyectoAutoPartes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAutoPartes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemillaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SemillaController> _logger;

        public SemillaController(ApplicationDbContext context, ILogger<SemillaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Endpoint para inicializar la base de datos con datos de ejemplo
        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarBaseDatos()
        {
            try
            {
                // Verificar si ya hay productos en la base de datos
                if (_context.Productos.Any())
                {
                    return BadRequest("La base de datos ya ha sido inicializada");
                }

                // Crear algunos productos de ejemplo
                var productos = new List<Producto>
                {
                    new Producto
                    {
                        Nombre = "Amortiguador delantero",
                        Precio = 350.00M,
                        Stock = 15,
                        Marca = "Monroe",
                        Año = "2020-2023",
                        Modelo = "Toyota Corolla",
                        Imagen = "amortiguador_delantero.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Filtro de aceite",
                        Precio = 85.75M,
                        Stock = 30,
                        Marca = "Fram",
                        Año = "2018-2023",
                        Modelo = "Honda Civic",
                        Imagen = "filtro_aceite.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Pastillas de freno",
                        Precio = 225.50M,
                        Stock = 20,
                        Marca = "Brembo",
                        Año = "2019-2023",
                        Modelo = "Mazda 3",
                        Imagen = "pastillas_freno.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Batería 12V",
                        Precio = 875.00M,
                        Stock = 8,
                        Marca = "Bosch",
                        Año = "2018-2023",
                        Modelo = "Nissan Sentra",
                        Imagen = "bateria.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Kit de embrague",
                        Precio = 1250.00M,
                        Stock = 5,
                        Marca = "LUK",
                        Año = "2017-2022",
                        Modelo = "Hyundai Elantra",
                        Imagen = "kit_embrague.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Radiador",
                        Precio = 950.75M,
                        Stock = 7,
                        Marca = "Valeo",
                        Año = "2019-2023",
                        Modelo = "Kia Forte",
                        Imagen = "radiador.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Bomba de agua",
                        Precio = 540.25M,
                        Stock = 12,
                        Marca = "GMB",
                        Año = "2018-2022",
                        Modelo = "Volkswagen Golf",
                        Imagen = "bomba_agua.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Alternador",
                        Precio = 1650.00M,
                        Stock = 6,
                        Marca = "Denso",
                        Año = "2019-2023",
                        Modelo = "Ford Focus",
                        Imagen = "alternador.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Bujías de encendido (set 4)",
                        Precio = 175.50M,
                        Stock = 25,
                        Marca = "NGK",
                        Año = "2017-2023",
                        Modelo = "Chevrolet Cruze",
                        Imagen = "bujias.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Correa de distribución",
                        Precio = 320.75M,
                        Stock = 14,
                        Marca = "Gates",
                        Año = "2018-2022",
                        Modelo = "Subaru Impreza",
                        Imagen = "correa_distribucion.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Sensor de oxígeno",
                        Precio = 195.25M,
                        Stock = 18,
                        Marca = "Delphi",
                        Año = "2019-2023",
                        Modelo = "Mitsubishi Lancer",
                        Imagen = "sensor_oxigeno.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Termostato",
                        Precio = 125.00M,
                        Stock = 22,
                        Marca = "Stant",
                        Año = "2017-2023",
                        Modelo = "Suzuki Swift",
                        Imagen = "termostato.jpg"
                    }
                };

                await _context.Productos.AddRangeAsync(productos);
                await _context.SaveChangesAsync();

                return Ok("Base de datos inicializada correctamente con productos de ejemplo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al inicializar la base de datos");
                return StatusCode(500, "Error interno del servidor al inicializar la base de datos");
            }
        }

        // Endpoint para verificar la conexión a la base de datos
        [HttpGet("verificar-conexion")]
        public IActionResult VerificarConexion()
        {
            try
            {
                // Intentar conectarse a la base de datos
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();

                return Ok("Conexión a la base de datos establecida correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar la conexión a la base de datos");
                return StatusCode(500, $"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Endpoint para crear/actualizar el esquema de la base de datos
        [HttpPost("migrar")]
        public IActionResult MigrarBaseDatos()
        {
            try
            {
                _context.Database.Migrate();
                return Ok("Migración de la base de datos completada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al migrar la base de datos");
                return StatusCode(500, $"Error al migrar la base de datos: {ex.Message}");
            }
        }
    }
}