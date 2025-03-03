using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms; // Asegurar el uso de MessageBox

namespace ProyectoAutoPartes
{
    public class claseGestionVentas
    {
        private claseGestionInventario inventario;
        private claseGestionVentas ventas;
        private claseClientes clientes;

        public Form2()
        {
            this.enlaceConexion = enlaceConexion;
            this.form = form;
        }

        // Método para cargar datos por fecha desde la base de datos MySQL (Migrado desde Access)
        // Se modificó la conexión y los parámetros para que sean compatibles con MySQL
        public void CargarDatosXFecha()
        { 
            string fechaSeleccionada = form.dateTimePickerVentas.Value.ToString("yyyy-MM-dd");
            try 
            {
                using (MySqlConnection con = new MySqlConnection(enlaceConexion))
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
            catch (Exception ex) 
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Método para cargar productos en un ComboBox desde MySQL
        public void CargarProductosEnComboBox(ComboBox comboBox)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(enlaceConexion))
                {
                    con.Open();
                    string query = "SELECT idProducto, nombreProducto FROM Productos";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    comboBox.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox.Items.Add(new { Text = reader["nombreProducto"].ToString(), Value = reader["idProducto"].ToString() });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        // Método para crear una factura en la lista enlazada
        public void CrearFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, int nofactura, string fechacompra, double pagoindividual, double pagototal)
        { 
            facturas.AgregarDatosFactura(idproducto, nombreproducto, nitcliente, cantidadllevada, nofactura, fechacompra, pagoindividual, pagototal);
        }
        
        // Método para realizar la compra (Ejecutar la lógica de la lista enlazada de facturas)
        public void RealizarCompra()
        {
            facturas.EfecturarCompra();
        }
    }
}
