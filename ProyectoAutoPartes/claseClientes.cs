using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProyectoAutoPartes
{
    public interface IFormDependencies
    {
        DataGridView dataGridViewClientes { get; }
    }

    public class ClaseClientes
    {
        private readonly string connectionString;
        private readonly IFormDependencies form;

        // Constructor con inyección de dependencias
        public ClaseClientes(string connectionString, IFormDependencies form)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.form = form ?? throw new ArgumentNullException(nameof(form));
        }

        // Cargar datos de clientes en el DataGridView y retornar DataTable
        public DataTable CargarDatos()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var adapter = new MySqlDataAdapter("SELECT * FROM Clientes", conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    form.dataGridViewClientes.DataSource = dt;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Búsqueda de cliente por nombre (retorna DataTable con resultados )
        public DataTable BuscarCliente(string nombre)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Clientes WHERE Nombre LIKE @nombre";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", $"%{nombre}%");

                        var adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        form.dataGridViewClientes.DataSource = dt;
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Guardar un nuevo cliente (retorna bool indicando éxito si esta es existente)
        public bool GuardarCliente(string dpiCliente, string nit, string nombre, string tipoCliente,
                                 string direccion, int comprasEmpresa, string telefono, double descuentosFidelidad)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(dpiCliente))
            {
                MessageBox.Show("Nombre y DPI son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Clientes 
                                    (DPI_Cliente, NIT, Nombre, TipoCliente, Direccion, 
                                     ComprasEnLaEmpresa, NumeroTelefonico, DescuentosFidelidad) 
                                    VALUES (@DPI, @NIT, @Nombre, @TipoCliente, @Direccion, 
                                            @Compras, @Telefono, @Descuentos)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DPI", dpiCliente);
                        command.Parameters.AddWithValue("@NIT", nit);
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@TipoCliente", tipoCliente);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Compras", comprasEmpresa);
                        command.Parameters.AddWithValue("@Telefono", telefono);
                        command.Parameters.AddWithValue("@Descuentos", descuentosFidelidad);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cliente guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Eliminar un cliente (retorna bool indicando éxito)
        public bool EliminarCliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Nombre no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Clientes WHERE Nombre = @Nombre";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                            return true;
                        }
                        MessageBox.Show("Cliente no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Procesar reembolso (retorna bool indicando éxito)
        public bool ProcesarReembolso(int idProducto, int cantidad, double costoUnitario, string nitCliente)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Registrar reembolso en la tabla Reembolsos si el producto existe
                            const string insertQuery = @"INSERT INTO Reembolsos 
                                                      (ID_Producto, Cantidad, CostoUnitario, FechaReembolso, NIT_Cliente) 
                                                      VALUES (@idProducto, @cantidad, @costoUnitario, NOW(), @nitCliente)";
                            using (var cmd = new MySqlCommand(insertQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                                cmd.Parameters.AddWithValue("@costoUnitario", costoUnitario);
                                cmd.Parameters.AddWithValue("@nitCliente", nitCliente);
                                cmd.ExecuteNonQuery();
                            }

                            // Actualizar inventario si el reembolso es exitoso
                            const string updateQuery = @"UPDATE Inventario 
                                                       SET CantidadEnStock = CantidadEnStock + @cantidad 
                                                       WHERE ID_Producto = @idProducto";
                            using (var cmd = new MySqlCommand(updateQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Reembolso procesado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error durante el reembolso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            // 
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}