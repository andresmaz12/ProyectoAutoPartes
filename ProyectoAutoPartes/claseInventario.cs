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
    public class claseGestionInventario // ya aprece la base de datos o no?
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
        public void InsertarDatos( string nombreProducto, string descripcion, int cantidadStock, 
                                  string especificacionVehiculo, double costo, double ganancia, double precio, int año)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Inventario (ID_Producto, NombreProducto, Descripcion, " +
                              "CantidadEnStock, EspecificacionVehiculo, Costo, Ganancia, Precio, Año) " +
                              "VALUES (@ID, @Nombre, @Desc, @Stock, @Espec, @Costo, @Ganancia, @Precio, @Año)";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Nombre", nombreProducto);
                command.Parameters.AddWithValue("@Desc", descripcion);
                command.Parameters.AddWithValue("@Stock", cantidadStock);
                command.Parameters.AddWithValue("@Espec", especificacionVehiculo);
                command.Parameters.AddWithValue("@Costo", costo);
                command.Parameters.AddWithValue("@Ganancia", ganancia);
                command.Parameters.AddWithValue("@Precio", precio);
                command.Parameters.AddWithValue("@Año", año);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Producto agregado al inventario con éxito!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar producto: " + ex.Message);
                }
            }
        }

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
                    string query = "UPDATE inventario SET NombreProducto = @nombre, Descripcion = @descripcion, Costo = @costo, CantidadEnStock = @stock WHERE ID_Producto = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", form7.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", form7.Descripcion);
                        cmd.Parameters.AddWithValue("@costo", form7.Precio); // Renombrado de 'Precio' a 'Costo'
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
                string query = "DELETE FROM inventario WHERE ID_Producto = @id"; // Corrección aquí

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
        public void ComprarInventario(string idProducto, int cantidadComprada, string proovedeor,double costoUnitario)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar la compra en la tabla Compras
                    string insertCompraQuery = "INSERT INTO Compras (ID_Producto, Cantidad, CostoUnitario, FechaCompra) VALUES (@ID_Producto, @Cantidad, @CostoUnitario, NOW())";
                    using (MySqlCommand cmdCompra = new MySqlCommand(insertCompraQuery, connection, transaction))
                    {
                        cmdCompra.Parameters.AddWithValue("@ID_Producto", idProducto);
                        cmdCompra.Parameters.AddWithValue("@Cantidad", cantidadComprada);
                        cmdCompra.Parameters.AddWithValue("@CostoUnitario", costoUnitario);
                        cmdCompra.ExecuteNonQuery();
                    }

                    // Actualizar el stock en la tabla Inventario
                    string updateInventarioQuery = "UPDATE Inventario SET CantidadEnStock = CantidadEnStock + @Cantidad WHERE ID_Producto = @ID_Producto";
                    using (MySqlCommand cmdInventario = new MySqlCommand(updateInventarioQuery, connection, transaction))
                    {
                        cmdInventario.Parameters.AddWithValue("@Cantidad", cantidadComprada);
                        cmdInventario.Parameters.AddWithValue("@ID_Producto", idProducto);
                        cmdInventario.ExecuteNonQuery();
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                    MessageBox.Show("Compra realizada y stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al realizar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}