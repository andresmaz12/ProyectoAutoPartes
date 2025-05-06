using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ProyectoAutoPartes
{
    public class calseGestionVentas
    {
        private readonly LinkedListVentas facturas;
        private readonly string connectionString;
        private readonly IFormDependencies form;

        // Constructor con inyección de dependencias
        public calseGestionVentas(string connectionString, IFormDependencies form)
        {
            this.connectionString = connectionString;
            this.form = form;
            this.facturas = new LinkedListVentas(connectionString); // Inicializando la lista enlazada
        }

        // Método para agregar producto a la lista temporal de ventas
        public bool AgregarProductoLista(string idProducto, string nombreProducto, string nitCliente,
                                       int cantidad, string noFactura, DateTime fechaVenta,
                                       double precioUnitario, double precioTotal)
        {
            try
            {
                facturas.AgregarVenta(idProducto, nombreProducto, nitCliente, cantidad,
                                    noFactura, fechaVenta, precioUnitario, precioTotal);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para cargar ventas por fecha (retorna DataTable en lugar de asignarlo directamente)
        public DataTable CargarDatosXFecha(DateTime fecha)
        {
            DataTable dataTable = new DataTable();
            string fechaSeleccionada = fecha.ToString("yyyy-MM-dd");

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Ventas WHERE DATE(fecha) = @fecha";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar datos: " + ex.Message);
            }

            return dataTable;
        }

        // Método para buscar factura (retorna DataTable)
        public DataTable BusquedaFactura(string noFactura)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Ventas WHERE NoFactura = @noFactura";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@noFactura", noFactura);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en búsqueda de factura: " + ex.Message);
            }

            return dataTable;
        }

        // Método para vaciar la lista temporal
        public void VaciarListaTemporal()
        {
            facturas.VaciarListaVentas();
        }

        // Método para eliminar elemento de la lista temporal
        public bool EliminarElementoLista(string idProducto)
        {
            try
            {
                facturas.EliminarVenta(idProducto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Método para procesar todas las ventas en la lista temporal
        // Modificación del método ProcesarVentas para asegurar transaccionalidad
        public bool ProcesarVentas()
        {
            try
            {
                // Verificar que haya ventas para procesar
                List<VentaTemporal> ventasTemporales = facturas.ObtenerVentasTemporales();
                if (ventasTemporales == null || ventasTemporales.Count == 0)
                {
                    MessageBox.Show("No hay productos en la lista de venta", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                // Establecer una conexión principal para manejar toda la transacción
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    MySqlTransaction transaccion = conexion.BeginTransaction();
                    
                    try
                    {
                        // 1. Actualizar inventario para cada producto vendido
                        foreach (var venta in ventasTemporales)
                        {
                            // Actualizar inventario reduciendo la cantidad vendida
                            string consultaActualizacion = "UPDATE Inventario SET CantidadEnStock = CantidadEnStock - @cantidad " +
                                                        "WHERE ID_Producto = @idProducto";
                            
                            using (MySqlCommand cmd = new MySqlCommand(consultaActualizacion, conexion, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmd.Parameters.AddWithValue("@idProducto", venta.IdProducto);
                                int filasAfectadas = cmd.ExecuteNonQuery();
                                
                                // Verificar si se actualizó correctamente el inventario
                                if (filasAfectadas <= 0)
                                {
                                    throw new Exception($"No se pudo actualizar el inventario para el producto ID: {venta.IdProducto}");
                                }
                                
                                // Verificar si hay suficiente stock después de la venta
                                string consultaVerificacion = "SELECT CantidadEnStock FROM Inventario WHERE ID_Producto = @idProducto";
                                using (MySqlCommand cmdVerificar = new MySqlCommand(consultaVerificacion, conexion, transaccion))
                                {
                                    cmdVerificar.Parameters.AddWithValue("@idProducto", venta.IdProducto);
                                    object resultado = cmdVerificar.ExecuteScalar();
                                    
                                    if (resultado != null && Convert.ToInt32(resultado) < 0)
                                    {
                                        throw new Exception($"Stock insuficiente para el producto ID: {venta.IdProducto}");
                                    }
                                }
                            }
                        }
                        
                        // 2. Procesar la venta principal y detalles
                        // Insertar la venta principal
                        string consultaVenta = "INSERT INTO Ventas (NoFactura, Fecha, PrecioTotal) VALUES (@noFactura, @fecha, @total)";
                        int idVentaGenerado = 0;
                        
                        using (MySqlCommand cmd = new MySqlCommand(consultaVenta, conexion, transaccion))
                        {
                            var primeraVenta = ventasTemporales.FirstOrDefault();
                            if (primeraVenta == null)
                            {
                                throw new Exception("No hay datos de venta para procesar");
                            }
                            
                            double precioTotal = ventasTemporales.Sum(v => v.PrecioUnitario * v.Cantidad);
                            
                            cmd.Parameters.AddWithValue("@noFactura", primeraVenta.NoFactura);
                            cmd.Parameters.AddWithValue("@fecha", primeraVenta.FechaVenta);
                            cmd.Parameters.AddWithValue("@total", precioTotal);
                            cmd.ExecuteNonQuery();
                            
                            // Obtener el ID generado
                            cmd.CommandText = "SELECT LAST_INSERT_ID()";
                            idVentaGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        
                        // Insertar detalles de venta
                        foreach (var venta in ventasTemporales)
                        {
                            string consultaDetalle = "INSERT INTO DetalleVentas (ID_Venta, ID_Producto, Cantidad, PrecioUnitario) " +
                                                "VALUES (@idVenta, @idProducto, @cantidad, @precio)";
                            
                            using (MySqlCommand cmd = new MySqlCommand(consultaDetalle, conexion, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@idVenta", idVentaGenerado);
                                cmd.Parameters.AddWithValue("@idProducto", venta.IdProducto);
                                cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmd.Parameters.AddWithValue("@precio", venta.PrecioUnitario);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        
                        // Si todo salió bien, confirmar la transacción
                        transaccion.Commit();
                        
                        // Limpiar la lista temporal una vez procesada
                        facturas.VaciarListaVentas();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Si algo falló, revertir todos los cambios
                        transaccion.Rollback();
                        
                        MessageBox.Show("Error al procesar la venta: " + ex.Message, 
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al procesar venta: " + ex.Message, 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Nuevo método para actualizar el inventario cuando se procesa una venta
        private void ActualizarInventarioPorVentas()
        {
            try
            {
                List<VentaTemporal> ventasTemporales = facturas.ObtenerVentasTemporales();

                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    MySqlTransaction transaccion = conexion.BeginTransaction();

                    try
                    {
                        foreach (var venta in ventasTemporales)
                        {
                            // Actualizar inventario reduciendo la cantidad vendida
                            string consultaActualizacion = "UPDATE Inventario SET CantidadEnStock = CantidadEnStock - @cantidad " +
                                                        "WHERE ID_Producto = @idProducto";
                            
                            using (MySqlCommand cmd = new MySqlCommand(consultaActualizacion, conexion, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmd.Parameters.AddWithValue("@idProducto", venta.IdProducto);
                                int filasAfectadas = cmd.ExecuteNonQuery();
                                
                                // Verificar si se actualizó correctamente el inventario
                                if (filasAfectadas <= 0)
                                {
                                    throw new Exception($"No se pudo actualizar el inventario para el producto ID: {venta.IdProducto}");
                                }
                                
                                // Verificar si hay suficiente stock después de la venta
                                string consultaVerificacion = "SELECT CantidadEnStock FROM Inventario WHERE ID_Producto = @idProducto";
                                using (MySqlCommand cmdVerificar = new MySqlCommand(consultaVerificacion, conexion, transaccion))
                                {
                                    cmdVerificar.Parameters.AddWithValue("@idProducto", venta.IdProducto);
                                    object resultado = cmdVerificar.ExecuteScalar();
                                    
                                    if (resultado != null && Convert.ToInt32(resultado) < 0)
                                    {
                                        throw new Exception($"Stock insuficiente para el producto ID: {venta.IdProducto}");
                                    }
                                }
                            }
                        }
                        
                        // Confirmar todas las actualizaciones si todo está correcto
                        transaccion.Commit();
                        MessageBox.Show("Inventario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Revertir todos los cambios si hay un error
                        transaccion.Rollback();
                        throw new Exception("Error al actualizar inventario: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la actualización del inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // Método para editar una venta existente en la BD
        public bool EditarVenta(int idVenta, string nuevoNoFactura, double nuevoPrecioTotal, DateTime nuevaFecha)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string query = @"UPDATE Ventas SET 
                                NoFactura = @NoFactura, 
                                PrecioTotal = @PrecioTotal, 
                                Fecha = @Fecha 
                                WHERE ID_Venta = @ID_Venta";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NoFactura", nuevoNoFactura);
                        cmd.Parameters.AddWithValue("@PrecioTotal", nuevoPrecioTotal);
                        cmd.Parameters.AddWithValue("@Fecha", nuevaFecha);
                        cmd.Parameters.AddWithValue("@ID_Venta", idVenta);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar venta: " + ex.Message);
            }
        }

        // Método para eliminar una venta de la BD
        public bool EliminarVenta(string idVenta)
        {
            try
            {
                // Primero recuperamos la información de la venta para actualizar el inventario
                DataTable datosVenta = ObtenerDetallesVenta(idVenta);
                
                if (datosVenta.Rows.Count > 0)
                {
                    using (MySqlConnection conexion = new MySqlConnection(connectionString))
                    {
                        conexion.Open();
                        MySqlTransaction transaccion = conexion.BeginTransaction();
                        
                        try
                        {
                            // Primero devolvemos los productos al inventario
                            foreach (DataRow fila in datosVenta.Rows)
                            {
                                string idProducto = fila["ID_Producto"].ToString();
                                int cantidad = Convert.ToInt32(fila["Cantidad"]);
                                
                                // Actualizar inventario sumando la cantidad de vuelta
                                string consultaActualizacion = "UPDATE Inventario SET CantidadEnStock = CantidadEnStock + @cantidad " +
                                                           "WHERE ID_Producto = @idProducto";
                                
                                using (MySqlCommand cmd = new MySqlCommand(consultaActualizacion, conexion, transaccion))
                                {
                                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            
                            // Luego eliminamos la venta
                            string queryEliminar = "DELETE FROM Ventas WHERE ID_Venta = @ID_Venta";
                            using (MySqlCommand cmd = new MySqlCommand(queryEliminar, conexion, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@ID_Venta", idVenta);
                                int filasAfectadas = cmd.ExecuteNonQuery();
                                
                                if (filasAfectadas > 0)
                                {
                                    transaccion.Commit();
                                    MessageBox.Show("Venta eliminada y stock restaurado correctamente.", "Éxito", 
                                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return true;
                                }
                                else
                                {
                                    transaccion.Rollback();
                                    MessageBox.Show("No se encontró la venta para eliminar.", "Error", 
                                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            throw new Exception("Error al eliminar venta: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la venta.", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar venta: " + ex.Message);
            }
        }

        // Método para obtener detalles de una venta específica
        private DataTable ObtenerDetallesVenta(string idVenta)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT ID_Producto, Cantidad FROM DetalleVentas WHERE ID_Venta = @idVenta";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles de venta: " + ex.Message);
            }

            return dataTable;
        }

        // Método para obtener el conteo actual de items en lista temporal
        public int ObtenerCantidadItemsTemporales()
        {
            return facturas.ObtenerCantidadVentas();
        }
    }
}