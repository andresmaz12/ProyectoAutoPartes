using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace ProyectoAutoPartes
{
    public class GestionInventario
    {
        private readonly string _connectionString;
        private readonly IFormDependencies form;

        public GestionInventario(string connectionString, IFormDependencies form)
        {
            _connectionString = connectionString;
            this.form = form;
        }

        // OPERACIONES BÁSICAS DE INVENTARIO
        public void InsertarProducto(string nombreProducto, string descripcion, int cantidadStock,
                                   string especificacionVehiculo, double costo, double ganancia,
                                   double precio, int año)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = @"INSERT INTO Inventario 
                                      (NombreProducto, Descripcion, CantidadEnStock, 
                                      EspecificacionVehiculo, Costo, Ganancia, Precio, Año) 
                                      VALUES (@Nombre, @Desc, @Stock, @Espec, @Costo, @Ganancia, @Precio, @Año)";

                using (var command = new MySqlCommand(query, connection))
                {
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
                        MessageBox.Show("Producto agregado al inventario con éxito!", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar producto: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void EditarProducto(int id, string nombre, string descripcion, double costo, int stock)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = @"UPDATE Inventario 
                                     SET NombreProducto = @nombre, 
                                         Descripcion = @descripcion, 
                                         Costo = @costo, 
                                         CantidadEnStock = @stock 
                                     WHERE ID_Producto = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@costo", costo);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                            MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el producto para actualizar.", "Advertencia",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar producto: " + ex.Message, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void EliminarProducto(int id)
        {
            var confirmacion = MessageBox.Show("¿Estás seguro de eliminar este producto?", "Confirmar",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = "DELETE FROM Inventario WHERE ID_Producto = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                            MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el producto para eliminar.", "Advertencia",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // BÚSQUEDAS Y CONSULTAS
        public void BuscarProducto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre para buscar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Inventario WHERE NombreProducto LIKE @nombre";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", $"%{nombre}%");

                    try
                    {
                        connection.Open();
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            _formMenu.dataGridViewInvetario.DataSource = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void BuscarProductosMultiples(List<string> nombresProductos)
        {
            if (nombresProductos == null || !nombresProductos.Any())
            {
                MessageBox.Show("Ingrese al menos un nombre de producto.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                var parametros = nombresProductos.Select((nombre, index) => $"@nombre{index}").ToList();
                var query = $"SELECT * FROM Inventario WHERE NombreProducto IN ({string.Join(",", parametros)})";

                using (var command = new MySqlCommand(query, connection))
                {
                    for (int i = 0; i < nombresProductos.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@nombre{i}", nombresProductos[i]);
                    }

                    try
                    {
                        connection.Open();
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            _formMenu.dataGridViewInvetario.DataSource = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error en la búsqueda múltiple: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // GESTIÓN DE STOCK
        public void ComprarProducto(int idProducto, int cantidad, double costoUnitario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Registrar la compra
                        const string insertQuery = @"INSERT INTO Compras 
                                                     (ID_Producto, Cantidad, CostoUnitario, FechaCompra) 
                                                     VALUES (@id, @cantidad, @costo, NOW())";

                        using (var command = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", idProducto);
                            command.Parameters.AddWithValue("@cantidad", cantidad);
                            command.Parameters.AddWithValue("@costo", costoUnitario);
                            command.ExecuteNonQuery();
                        }

                        // Actualizar inventario
                        const string updateQuery = @"UPDATE Inventario 
                                                   SET CantidadEnStock = CantidadEnStock + @cantidad 
                                                   WHERE ID_Producto = @id";

                        using (var command = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", idProducto);
                            command.Parameters.AddWithValue("@cantidad", cantidad);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Compra registrada y stock actualizado.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al procesar compra: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void ReintegrarReembolso(int idProducto, int cantidad, double costoUnitario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Registrar el reembolso
                        const string insertQuery = @"INSERT INTO Reembolsos 
                                                   (ID_Producto, Cantidad, CostoUnitario, FechaRembolso) 
                                                   VALUES (@id, @cantidad, @costo, NOW())";

                        using (var command = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", idProducto);
                            command.Parameters.AddWithValue("@cantidad", cantidad);
                            command.Parameters.AddWithValue("@costo", costoUnitario);
                            command.ExecuteNonQuery();
                        }

                        // Actualizar inventario
                        const string updateQuery = @"UPDATE Inventario 
                                                   SET CantidadEnStock = CantidadEnStock - @cantidad 
                                                   WHERE ID_Producto = @id";

                        using (var command = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", idProducto);
                            command.Parameters.AddWithValue("@cantidad", cantidad);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Reembolso procesado y stock actualizado.", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al procesar reembolso: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // REPORTES Y ANÁLISIS
        public double CalcularValorTotalInventario()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = "SELECT SUM(CantidadEnStock * Costo) FROM Inventario";

                using (var command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDouble(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al calcular valor total: " + ex.Message, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return 0;
                    }
                }
            }
        }

        public void IdentificarProductosConStockBajo(int limite)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Inventario WHERE CantidadEnStock < @limite";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@limite", limite);

                    try
                    {
                        connection.Open();
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            _formMenu.dataGridViewInvetario.DataSource = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar productos con stock bajo: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}