using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ProyectoAutoPartes
{
    public class claseGestionRRHH
    {
        //Dirección de la base de datos 
        private string connectionString = "D:\\Base de datos VB\\ProyectoAutoPartes\\Avamce.... de proyecto.prueba6.mwb";

        private formMenu form;

        // Constructor con inyección de dependencias
        public claseGestionRRHH(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        public bool VerificarUsuario(string usuario, string contraseña)
        {
            bool accesoPermitido = false;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Contraseña FROM Empleados WHERE Usuario = @usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    conn.Open();
                    object resultado = cmd.ExecuteScalar(); // Obtiene la contraseña almacenada

                    if (resultado != null)
                    {
                        string contraseñaGuardada = resultado.ToString();

                        // Comparar contraseñas (si usas hashing, aquí aplicarías la verificación)
                        if (contraseña == contraseñaGuardada)
                        {
                            accesoPermitido = true;
                        }
                    }
                }
            }

            return accesoPermitido;
        }

    }
}