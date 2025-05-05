using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace ProyectoAutoPartes
{
    public class ClaseClientes
    {
        private readonly string connectionString;
        private readonly IFormDependencies form;

        // Constructor with dependency injection
        public ClaseClientes(string connectionString, IFormDependencies formDependencies)
        {
            this.connectionString = connectionString;
            this.form = formDependencies;
        }

        // Load all clients (returns DataTable)
        public DataTable CargarDatos()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var adapter = new MySqlDataAdapter("SELECT * FROM Clientes", conn))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading data: {ex.Message}");
            }
        }

        // Search client by name (returns DataTable)
        public DataTable BuscarCliente(string nombre)
        {
            DataTable dt = new DataTable();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return CargarDatos();
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Clientes WHERE Nombre LIKE @Nombre";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", $"%{nombre}%");

                        var adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching client: {ex.Message}");
            }
        }

        // Save new client (returns success bool)
        public bool GuardarCliente(
            string dpiCliente,
            string nit,
            string nombre,
            string tipoCliente,
            string direccion,
            int comprasEmpresa,
            string telefono,
            double descuentosFidelidad)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(dpiCliente))
            {
                throw new ArgumentException("Name and DPI are required.");
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
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving client: {ex.Message}");
            }
        }

        // Delete client (returns success bool)
        public bool EliminarClientes(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Name cannot be empty.");
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
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting client: {ex.Message}");
            }
        }

        // Process refund (returns success bool)
        public bool ProcesarReembolso(
            int idProducto,
            int cantidad,
            double costoUnitario,
            string nitCliente)
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
                            // Register refund
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

                            // Update inventory
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
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing refund: {ex.Message}");
            }
        }
    }
}