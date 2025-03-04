using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms; // Asegurar el uso de MessageBox

namespace ProyectoAutoPartes
{
    public class claseGestionInventario
    {
        private string connectionString;
        private formMenu form;

        // Constructor con inyección de dependencias
        public claseGestionInventario(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        // Método para insertar datos en la base de datos (Migrado de Access a MySQL)
        // Se cambiaron OleDbConnection y OleDbCommand por MySqlConnection y MySqlCommand
        // Se cambiaron los parámetros de '?' a '@nombre', '@descripcion', etc.
        public void InsertarDatos(string nombre, string descripcion, double costo, double ganancia, int stock, string ruta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO Productos (Nombre_producto, Descripcion, Precio_Unitario, Ganancia, Stock_Actual) VALUES (@nombre, @descripcion, @costo, @ganancia, @stock)";
                    using var cmd = new MySqlCommand(query, conn);

                    // Se asignan los valores a los parámetros
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@costo", costo);
                    cmd.Parameters.AddWithValue("@ganancia", ganancia);
                    cmd.Parameters.AddWithValue("@stock", stock);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Datos insertados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Método para buscar elementos (Migrado de Access a MySQL)
        // Se cambiaron los '?' por '@nombre' y '@precio' en la consulta SQL
        public void BuscarElemento()
        {
            // Obtener el nombre desde un InputBox
            string nombre = Interaction.InputBox("Ingrese el nombre del producto a buscar:", "Búsqueda de Producto", "").Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Debe ingresar un nombre para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM Productos WHERE Nombre_producto LIKE @nombre";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Se abre explícitamente la conexión

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", $"%{nombre}%"); 

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        form.dataGridViewInvetario.DataSource = dt; 
                    }
                }
            }
        }


        // Método para editar datos (Migrado de Access a MySQL)
        // Se reemplazaron OleDb por MySql en conexiones y comandos
        public void EditarDatos()
        {
            formularioEditarDatos form7 = new formularioEditarDatos();
            DialogResult result = form7.ShowDialog();

            if (result == DialogResult.OK)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Productos SET Nombre_producto = @nombre, Descripcion = @descripcion, Precio_Unitario = @precio, Stock_Actual = @stock WHERE Id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", form7.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", form7.Descripcion);
                        cmd.Parameters.AddWithValue("@precio", form7.Precio);
                        cmd.Parameters.AddWithValue("@stock", form7.Cantidad);
                        cmd.Parameters.AddWithValue("@id", form7.ID);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                            MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el producto para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        // Método para eliminar datos (Migrado de Access a MySQL)
        // Se ajustaron comandos para que sean compatibles con MySQL
        public void EliminarDatos()
        {
            // Obtener el ID desde un InputBox
            string inputID = Interaction.InputBox("Ingrese el ID del producto que desea borrar:", "Eliminar Producto", "").Trim();

            // Validar que el ID sea un número válido
            if (!int.TryParse(inputID, out int ID))
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Productos WHERE Id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID); 

                    // Confirmación antes de eliminar
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("No se encontró el producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

    }
}