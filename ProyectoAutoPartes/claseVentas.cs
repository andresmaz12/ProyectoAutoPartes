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
        public bool ProcesarVentas()
        {
            try
            {
                facturas.ProcesarVentas();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar ventas: " + ex.Message);
            }
        }

        // Método para editar una venta existente en la BD
        public bool EditarVenta(int idVenta, string nuevoNoFactura, double nuevoPrecioTotal, DateTime nuevaFecha)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Ventas SET 
                                NoFactura = @NoFactura, 
                                PrecioTotal = @PrecioTotal, 
                                Fecha = @Fecha 
                                WHERE ID_Venta = @ID_Venta";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
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
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Ventas WHERE ID_Venta = @ID_Venta";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID_Venta", idVenta);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar venta: " + ex.Message);
            }
        }

        // Método para obtener el conteo actual de items en lista temporal
        public int ObtenerCantidadItemsTemporales()
        {
            return facturas.ObtenerCantidadVentas();
        }
    }
}
