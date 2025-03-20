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
    public class claseGestionVentas
    {
        //Llama a la lista enlazada para realizar 
        linkedListFacturas facturas = new linkedListFacturas();
        //Dirección de la base de datos 
        private string connectionString = "Server=localHost; ";

        private formMenu form;

        // Constructor con inyección de dependencias
        public claseGestionVentas(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        public void AgregarProductoLista()
        {

        }

        public void CargarDatosXFecha()
        { 
            string fechaSeleccionada = form.dateTimePickerVentas.Value.ToString("yyyy-MM-dd");
            try 
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Ventas WHERE DATE(fecha) = @fecha";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    form.dataGridViewVentas.DataSource = dataTable;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error: " + ex.Message);
            }
                
        }

        public void BusquedaFactura(string factura)
        {
            
        }

        public void VaciarLista()
        {
            facturas.VaciarLista();
        }

        public void EliminiarElemento(string elemento)
        {
            //Se usara un inputBox para obtener el id del producto 
            facturas.EliminarProducto(elemento);
        }
        public void CrearFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, DateTime fechacompra, double pagoindividual, double pagototal)
        { 
            facturas.AgregarDatosFactura(idproducto, nombreproducto, nitcliente, cantidadllevada, nofactura, fechacompra, pagoindividual, pagototal);
        }
        
        public void RealizarCompra()
        {
            facturas.EfecturarCompra();
        }
        public void EditarVenta(int idVenta, string nuevoNoFactura, double nuevoPrecioTotal, DateTime nuevaFecha)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Ventas SET NoFactura = @NoFactura, PrecioTotal = @PrecioTotal, Fecha = @Fecha WHERE ID_Venta = @ID_Venta";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@NoFactura", nuevoNoFactura);
                        cmd.Parameters.AddWithValue("@PrecioTotal", nuevoPrecioTotal);
                        cmd.Parameters.AddWithValue("@Fecha", nuevaFecha);
                        cmd.Parameters.AddWithValue("@ID_Venta", idVenta);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                            MessageBox.Show("Venta editada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró la venta a editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void EliminarVenta(string idVenta)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Ventas WHERE ID_Venta = @ID_Venta";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID_Venta", idVenta);

                        DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta venta?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            int filasAfectadas = cmd.ExecuteNonQuery();
                            if (filasAfectadas > 0)
                                MessageBox.Show("Venta eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("No se encontró la venta a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
