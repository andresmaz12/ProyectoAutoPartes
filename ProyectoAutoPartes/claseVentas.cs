using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
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
        private string connectionString = "Dirreccion de la base de datos";

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

        public void VaciarLista()
        {
            facturas.VaciarLista();
        }

        public void EliminiarElemento(string elemento)
        {
            facturas.EliminarProducto(elemento);
        }
        public void CrearFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, string fechacompra, double pagoindividual, double pagototal)
        { 
            facturas.AgregarDatosFactura(idproducto, nombreproducto, nitcliente, cantidadllevada, nofactura, fechacompra, pagoindividual, pagototal);
        }
        
        public void RealizarCompra()
        {
            facturas.EfecturarCompra();
        }
    }
}
