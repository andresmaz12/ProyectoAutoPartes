using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            form.dataGridView3.DataSource = dt;
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

            if (reader.Read()) // Si se encontró un cliente con el nombre dado
            {
                form.textBox6.Text = reader["Telefono"].ToString();
                form.textBox7.Text = reader["NIT"].ToString();
                form.textBox8.Text = reader["Direccion"].ToString();
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.");
            }

            conn.Close();
        }

        public void GuardarCliente()
        {
            // Verifica que todos los campos estén llenos antes de guardar
            if (string.IsNullOrWhiteSpace(form.textBox5.Text) || string.IsNullOrWhiteSpace(form.textBox6.Text) ||
                string.IsNullOrWhiteSpace(form.textBox7.Text) || string.IsNullOrWhiteSpace(form.textBox8.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

             using var conn = new MySqlConnection(connectionString);
            string query = "INSERT INTO Clientes (Nombre, Telefono, NIT, Direccion) VALUES (?, ?, ?, ?)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("?", form.textBox5.Text);
            cmd.Parameters.AddWithValue("?", form.textBox6.Text);
            cmd.Parameters.AddWithValue("?", form.textBox7.Text);
            cmd.Parameters.AddWithValue("?", form.textBox8.Text);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            CargarDatos(); // Refresca la lista de clientes en el DataGridView
            MessageBox.Show("Cliente guardado con éxito.");
        }
    }
}