using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutoPartes
{
    public class claseFuncionesVarias
    {
        private string connectionString = "D:\\Base de datos VB\\ProyectoAutoPartes\\Avamce.... de proyecto.prueba6.mwb";

        public void GitCommitPush(string mensaje)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process proceso = new Process { StartInfo = psi };
            proceso.Start();

            using (StreamWriter sw = proceso.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("git add backup.sql");
                    sw.WriteLine($"git commit -m \"{mensaje}\"");
                    sw.WriteLine("git push origin main");
                }
            }

            proceso.WaitForExit();
        }

        public bool VerificarCredenciales(string usuario, string contraseña)
        {
            // Inicializamos el resultado como falso
            bool credencialesValidas = false;

            // Usamos la misma cadena de conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Consulta SQL para verificar si existe el usuario con esa contraseña
                    string query = "SELECT COUNT(*) FROM RRHH WHERE Usuario = @usuario AND Contraseña = @password";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Añadimos los parámetros para evitar inyección SQL
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@password", contraseña);

                    // Abrimos la conexión
                    connection.Open();

                    // Ejecutamos la consulta y obtenemos el número de coincidencias
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Si hay al menos una coincidencia, las credenciales son válidas
                    if (count > 0)
                    {
                        credencialesValidas = true;
                    }
                }
                catch (Exception ex)
                {
                    // Manejamos cualquier error que pueda ocurrir
                    MessageBox.Show("Error al verificar credenciales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return credencialesValidas;
        }
    }
}
