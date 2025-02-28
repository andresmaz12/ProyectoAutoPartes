using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Método para insertar datos en la base de datos
        public void InsertarDatos(string nombre, string descripcion, double precio, int stock)
        {
            try
            {
                // Abre la conexión con la base de datos
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    // Consulta SQL ajustada para los nombres de las columnas
                    string query = "INSERT INTO Productos (Nombre_producto, Descripcion, Precio_Unitario, Stock_Actual) VALUES (?, ?, ?, ?)";
                    using var cmd = new OleDbCommand(query, conn);

                    // Asignar los valores a los parámetros en el orden correcto
                    cmd.Parameters.AddWithValue("?", nombre);
                    cmd.Parameters.AddWithValue("?", descripcion);
                    cmd.Parameters.AddWithValue("?", precio);  // Para Precio_Unitario, que es de tipo moneda
                    cmd.Parameters.AddWithValue("?", stock);   // Para Stock_Actual, que es de tipo entero

                    // Ejecutar la consulta
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Verificación: Mostrar un mensaje si la inserción fue exitosa
                    MessageBox.Show("Datos insertados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Si algo salió mal, muestra el mensaje de error
                MessageBox.Show($"Ocurrió un error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Metodo para cargar los datos en datagridview
        public void CargarDatos()
        {

            using var conn = new OleDbConnection(connectionString);
            string query = "SELECT * FROM Productos"; //Aqui se llama a la tabla que se quiere usar, es posible reutilizar el codigo en caso de ser necesesario
            var adapter = new OleDbDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            form.dataGridView1.DataSource = dt;
        }

        public void BuscarElemento()
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();


            // Obtener los valores de los TextBox
            string nombre = form4.Nombre;
            string precio = form4.Precio;

            // Crear la consulta SQL
            string query = "SELECT * FROM Productos WHERE 1=1"; // La condición 1=1 es para que la consulta sea válida si no se proporcionan criterios

            // Añadir condiciones adicionales si los TextBox tienen valores
            if (!string.IsNullOrEmpty(nombre))
            {
                query += " AND Nombre_producto LIKE ?";
            }
            if (!string.IsNullOrEmpty(precio))
            {
                query += " AND Precio_Unitario = ?";
            }

            // Ejecutar la consulta
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    // Agregar los parámetros de la consulta, en el orden correcto
                    adapter.SelectCommand.Parameters.Clear();
                    if (!string.IsNullOrEmpty(nombre))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("?", "%" + nombre + "%");
                    }
                    if (!string.IsNullOrEmpty(precio))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("?", Convert.ToDouble(precio)); // Asegurando que el precio sea un número
                    }

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Mostrar los resultados en el DataGridView
                    form.dataGridView1.DataSource = dt;
                }
            }
        }

        public void ReestableceDatos()
        {
            try
            {
                // Llamar al método que carga todos los datos en el DataGridView
                CargarDatos();

                MessageBox.Show("Los datos han sido reestablecidos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Ocurrió un error al reestablecer los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EditarDatos()
        {
            Form7 form7 = new Form7();
            DialogResult result = form7.ShowDialog();

            // Verificar si el usuario guardó los datos antes de cerrar el formulario
            if (result == DialogResult.OK)
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Sentencia UPDATE corregida
                    string query = "UPDATE Productos SET Nombre_producto = ?, Descripcion = ?, Precio_Unitario = ?, Stock_Actual = ? WHERE Id = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Asignar valores a los parámetros en el orden correcto
                        cmd.Parameters.AddWithValue("?", form7.Nombre);
                        cmd.Parameters.AddWithValue("?", form7.Descripcion);
                        cmd.Parameters.AddWithValue("?", form7.Precio);  // Para Precio_Unitario (tipo moneda)
                        cmd.Parameters.AddWithValue("?", form7.Cantidad); // Para Stock_Actual (tipo entero)
                        cmd.Parameters.AddWithValue("?", form7.ID); // El ID del producto a actualizar

                        // Ejecutar la consulta
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        // Verificación: Mostrar un mensaje según el resultado
                        if (filasAfectadas > 0)
                            MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el producto para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        public void EliminarDatos()
        {
            Form6 form6 = new Form6();
            form6.ShowDialog();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Sentencia DELETE para eliminar un producto por su ID
                string query = "DELETE FROM Productos WHERE Id = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Solo añadimos un parámetro, que es el ID
                    cmd.Parameters.AddWithValue("?", form6.ID);

                    // Preguntar al usuario antes de eliminar
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?",
                        "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (filasAfectadas > 0)
                            MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el producto para eliminar.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
