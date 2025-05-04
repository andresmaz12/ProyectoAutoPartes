using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProyectoAutoPartes
{
    public class classeProveedores
    {
        private string connectionString;
        private formMenu form;

        public classeProveedores(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
         }

        public DataTable GetTablaProveedores()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM proveedores";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void InsertarProveedor(string Nit, string Nombre, string Telefono, string Tipo, string Direccion)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO proveedores (nit, nombre, telefono, tipo, direccion) 
                            VALUES (@Nit, @Nombre, @Telefono, @Tipo, @Direccion)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nit", Nit);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    cmd.Parameters.AddWithValue("@Direccion", Direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable BuscarProveedores(string criterio)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT * FROM proveedores 
                            WHERE nit LIKE @Criterio OR 
                                  nombre LIKE @Criterio OR 
                                  telefono LIKE @Criterio OR 
                                  tipo LIKE @Criterio OR 
                                  direccion LIKE @Criterio";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Criterio", "%" + criterio + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable BuscarProveedorPorId(int id)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM proveedores WHERE id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool EliminarProveedor(int id)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM proveedores WHERE id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected > 0;
        }

        public bool ActualizarProveedor(int id, string Nit, string Nombre, string Telefono, string Tipo, string Direccion)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE proveedores 
                            SET nit = @Nit, 
                                nombre = @Nombre, 
                                telefono = @Telefono, 
                                tipo = @Tipo, 
                                direccion = @Direccion 
                            WHERE id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nit", Nit);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    cmd.Parameters.AddWithValue("@Direccion", Direccion);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected > 0;
        }
    }
}
