using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Globalization;

namespace ProyectoAutoPartes 
{
    public class claseGestionRRHH
    {
        //Dirección de la base de datos 
        private readonly string connectionString;
        private readonly IFormDependencies form;

        // Constructor con inyección de dependencias
        public claseGestionRRHH(string connectionString, IFormDependencies form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        public bool VerificarUsuario(string usuario, string contraseÃ±a)
        {
            bool accesoPermitido = false;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT ContraseÃ±a FROM Empleados WHERE Usuario = @usuario";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        conn.Open();
                        object resultado = cmd.ExecuteScalar(); // Obtiene la contraseÃ±a almacenada

                        if (resultado != null)
                        {
                            string contraseÃ±aGuardada = resultado.ToString();

                            // Comparar contraseÃ±as
                            if (contraseÃ±a == contraseÃ±aGuardada)
                            {
                                accesoPermitido = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al verificar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return accesoPermitido;
        }

        public DataTable BuscarEmpleado(string nombre)
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados 
                                WHERE 
                                    Nombre LIKE @Nombre
                                ORDER BY 
                                    Apellido, Nombre";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        // Usamos LIKE para buscar coincidencias parciales
                        comando.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                if (dtResultado.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron empleados con ese nombre", "BÃºsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool AgregarEmpleado(string dpiEmpleado, string nombre, DateTime fechaNacimiento, string rol, 
                                string cuentaBancaria, string usuario, string contraseÃ±a, int faltas, double bonos, double salario)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Crear ID automÃ¡tico basado en el DPI (ultimos 4 nÃºmeros)
                    string idEmpleado = "";
                    if (dpiEmpleado.Length >= 4)
                    {
                        idEmpleado = dpiEmpleado.Substring(dpiEmpleado.Length - 4);
                    }
                    else
                    {
                        idEmpleado = dpiEmpleado;
                    }

                    string query = "INSERT INTO Empleados (ID, DPI_Empleado, Nombre, " +
                                  "FechaDeNacimiento, Rol, CuentaBancaria, Usuario, ContraseÃ±a, NumeroFaltas, Bonos, Salario, FechaContratacion) " +
                                  "VALUES (@ID, @DPI, @Nombre, @Fecha, @Rol, @Cuenta, @Usuario, @Password, @Faltas, @Bonos, @Salario, @FechaContratacion)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ID", idEmpleado);
                    command.Parameters.AddWithValue("@DPI", dpiEmpleado);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Fecha", fechaNacimiento.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Rol", rol);
                    command.Parameters.AddWithValue("@Cuenta", cuentaBancaria);
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Password", contraseÃ±a);
                    command.Parameters.AddWithValue("@Faltas", faltas);
                    command.Parameters.AddWithValue("@Bonos", bonos);
                    command.Parameters.AddWithValue("@Salario", salario);
                    command.Parameters.AddWithValue("@FechaContratacion", DateTime.Now.ToString("yyyy-MM-dd"));

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void ModificarLlavesAcceso(string idEmpleado, string nuevoUsuario, string nuevaContraseÃ±a)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE Empleados SET Usuario = @Usuario, ContraseÃ±a = @Password WHERE ID = @ID";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", nuevoUsuario);
                    command.Parameters.AddWithValue("@Password", nuevaContraseÃ±a);
                    command.Parameters.AddWithValue("@ID", idEmpleado);

                    connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Credenciales actualizadas correctamente", "Ãxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontrÃ³ el empleado con el ID especificado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar credenciales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int SeleccionarNivel(string rol)
        {
            int nivelAcceso = 5; // Nivel predeterminado (mÃ¡s restrictivo)
            
            // Asignamos nivel segÃºn el rol (1 es el mÃ¡s alto/administrativo, 5 el mÃ¡s bajo)
            switch (rol.ToLower())
            {
                case "gerente":
                case "administrador":
                    nivelAcceso = 1;
                    break;
                case "supervisor":
                case "jefe":
                    nivelAcceso = 2;
                    break;
                case "vendedor senior":
                    nivelAcceso = 3;
                    break;
                case "vendedor":
                    nivelAcceso = 4;
                    break;
                case "asistente":
                case "bodeguero":
                default:
                    nivelAcceso = 5;
                    break;
            }
            
            return nivelAcceso;
        }

        public double Salario()
        {
            string input = Interaction.InputBox("Ingrese el salario del empleado", "InscripciÃ³n Empleado", "");

            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double salario))
            {
                if (salario <= 0)
                {
                    MessageBox.Show("El salario debe ser mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Salario(); // RecursiÃ³n para volver a pedir el salario
                }
                
                MessageBox.Show("Salario agregado correctamente", "InscripciÃ³n de empleado", MessageBoxButtons.OK);
                return salario; // Retorna el salario vÃ¡lido
            }
            else
            {
                MessageBox.Show("Ingrese un nÃºmero vÃ¡lido para el salario", "InscripciÃ³n de empleado", MessageBoxButtons.OK);
                return Salario(); // Vuelve a pedir el salario hasta que sea vÃ¡lido
            }
        }

        public bool EsFechaValida(string fecha)
        {
            // Intentamos convertir la fecha segÃºn diversos formatos comunes
            string[] formatos = { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "dd-MM-yyyy" };
            
            return DateTime.TryParseExact(fecha, formatos, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida);
        }

        /// <summary>
        /// Filtra empleados por salario mayor o igual al especificado
        /// </summary>
        /// <param name="salarioMinimo">Salario mÃ­nimo para filtrar</param>
        /// <returns>DataTable con los empleados que cumplen el criterio</returns>
        public DataTable FiltrarSalario(int salarioMinimo)
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados 
                                WHERE 
                                    Salario >= @SalarioMinimo
                                ORDER BY 
                                    Salario DESC";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@SalarioMinimo", salarioMinimo);

                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar por salario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Filtra empleados por rol especÃ­fico
        /// </summary>
        /// <param name="rol">Rol para filtrar</param>
        /// <returns>DataTable con los empleados que cumplen el criterio</returns>
        public DataTable FiltrarPorRol(string rol)
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados 
                                WHERE 
                                    Rol LIKE @Rol
                                ORDER BY 
                                    Apellido, Nombre";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        // Usamos LIKE para buscar coincidencias parciales
                        comando.Parameters.AddWithValue("@Rol", "%" + rol + "%");

                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar por rol: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Filtra empleados por nÃºmero de faltas mayor o igual al especificado
        /// </summary>
        /// <param name="faltasMinimo">NÃºmero mÃ­nimo de faltas para filtrar</param>
        /// <returns>DataTable con los empleados que cumplen el criterio</returns>
        public DataTable FiltrarPorFaltas(int faltasMinimo)
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados 
                                WHERE 
                                    NumeroFaltas >= @FaltasMinimo
                                ORDER BY 
                                    NumeroFaltas DESC";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@FaltasMinimo", faltasMinimo);

                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar por faltas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Filtra empleados por nÃºmero de ventas mayor o igual al especificado
        /// </summary>
        /// <param name="ventasMinimo">NÃºmero mÃ­nimo de ventas para filtrar</param>
        /// <returns>DataTable con los empleados que cumplen el criterio</returns>
        public DataTable FiltrarPorVentas(int ventasMinimo)
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados 
                                WHERE 
                                    NumeroVentas >= @VentasMinimo
                                ORDER BY 
                                    NumeroVentas DESC";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@VentasMinimo", ventasMinimo);

                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar por ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// MÃ©todo adicional para cargar todos los empleados
        /// </summary>
        /// <returns>DataTable con todos los empleados</returns>
        public DataTable CargarTodosEmpleados()
        {
            DataTable dtResultado = new DataTable();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    ID, 
                                    Nombre, 
                                    Apellido, 
                                    Rol, 
                                    Salario, 
                                    FechaContratacion, 
                                    NumeroFaltas, 
                                    NumeroVentas
                                FROM 
                                    Empleados
                                ORDER BY 
                                    Apellido, Nombre";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                        adaptador.Fill(dtResultado);
                    }
                }

                return dtResultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar todos los empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        
        /// <summary>
        /// Elimina un empleado por su ID
        /// </summary>
        /// <param name="idEmpleado">ID del empleado a eliminar</param>
        /// <returns>True si se eliminÃ³ correctamente, False en caso contrario</returns>
        public bool EliminarEmpleado(string idEmpleado)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Empleados WHERE ID = @ID";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idEmpleado);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Empleado eliminado correctamente", "Ãxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontrÃ³ un empleado con ese ID", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        /// <summary>
        /// Actualiza la informaciÃ³n de un empleado
        /// </summary>
        public bool ActualizarEmpleado(string idEmpleado, string nombre, string rol, string cuentaBancaria, 
                                    double salario, int faltas, double bonos)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE Empleados 
                                  SET Nombre = @Nombre, 
                                      Rol = @Rol, 
                                      CuentaBancaria = @Cuenta, 
                                      Salario = @Salario, 
                                      NumeroFaltas = @Faltas, 
                                      Bonos = @Bonos 
                                  WHERE ID = @ID";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idEmpleado);
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Rol", rol);
                        comando.Parameters.AddWithValue("@Cuenta", cuentaBancaria);
                        comando.Parameters.AddWithValue("@Salario", salario);
                        comando.Parameters.AddWithValue("@Faltas", faltas);
                        comando.Parameters.AddWithValue("@Bonos", bonos);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("InformaciÃ³n del empleado actualizada correctamente", "Ãxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontrÃ³ un empleado con ese ID", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}