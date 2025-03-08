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

        public void GuardarCliente(string nombre, string telefono, string nit, string direccion)
        {
            // Verifica que todos los campos estén llenos antes de guardar
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(nit) || string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            using var conn = new MySqlConnection(connectionString);
            string query = "INSERT INTO Clientes (Nombre, Telefono, NIT, Direccion) VALUES (@nombre, @telefono, @nit, @direccion)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@telefono", telefono);
            cmd.Parameters.AddWithValue("@nit", nit);
            cmd.Parameters.AddWithValue("@direccion", direccion);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            CargarDatos(); // Refresca la lista de clientes en el DataGridView
            MessageBox.Show("Cliente guardado con éxito.");
        }
    }
}