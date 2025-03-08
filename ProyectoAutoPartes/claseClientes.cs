using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProyectoAutoPartes
{
    public class claseClientes
    {
        private string connectionString;
        private formMenu form;

        // Constructor con inyección de dependencias
        public claseClientes(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        public void CargarDatos()
        {
            using var conn = new MySqlConnection(connectionString);
            string query = "SELECT * FROM Clientes"; //Aqui se llama a la tabla que se quiere usar, es posible reutilizar el codigo en caso de ser necesesario
            var adapter = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            form.dataGridViewClientes.DataSource = dt;
        }

        public void BuscarCliente(string nombre)
        { 
            if (string.IsNullOrWhiteSpace(nombre)) return; // Verifica que el campo de nombre no esté vacío
            using var conn = new MySqlConnection(connectionString);
            string query = "SELECT * FROM Clientes WHERE Nombre = ?";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", nombre); // Asigna el parámetro con el nombre del cliente

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            conn.Close();
        }

        public void GuardarCliente(string dpiCliente, string nit, string nombre, string tipoCliente,
                              string direccion, bool comprasEmpresa, string telefono, double descuentosFidelidad)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Clientes (DPI_Cliente, NIT, Nombre, TipoCliente, Direccion, " +
                              "ComprasEnLaEmpresa, NumeroTelefonico, DescuentosFidelidad) " +
                              "VALUES (@DPI, @NIT, @Nombre, @TipoCliente, @Direccion, @Compras, @Telefono, @Descuentos)";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@DPI", dpiCliente);
                command.Parameters.AddWithValue("@NIT", nit);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@TipoCliente", tipoCliente);
                command.Parameters.AddWithValue("@Direccion", direccion);
                command.Parameters.AddWithValue("@Compras", comprasEmpresa);
                command.Parameters.AddWithValue("@Telefono", telefono);
                command.Parameters.AddWithValue("@Descuentos", descuentosFidelidad);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cliente agregado con éxito!");
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar cliente: " + ex.Message);
                }
            }
        }
    }
}