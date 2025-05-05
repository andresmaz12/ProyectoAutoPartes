using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ProyectoAutoPartes
{
    

    

    public partial class verificarUsuarioContrasenia : Form
    {
        private string connectionString;
        private formMenu form;

        public verificarUsuarioContrasenia()
        {
        }


        // Constructor con inyección de dependencias
        public verificarUsuarioContrasenia(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
            InitializeComponent();
        }

        //Constructores de clase
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public bool UsuarioValido { get; set; }
        public int NivelEmpleado { get; set; }


        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Ingrese un valor valido para el usuario", "Error", MessageBoxButtons.OK);
                MessageBox.Show("Ingrese un valor valido para el usuario", "Error", MessageBoxButtons.OK);
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                Usuario = textBox1.Text;
                Contrasenia = textBox2.Text;
                

                int nivel = 0;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT nivel FROM empleados WHERE usuario = @usuario AND contrasena = @contrasena";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", Usuario);
                        cmd.Parameters.AddWithValue("@contrasena", Contrasenia);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            nivel = Convert.ToInt32(result);
                            NivelEmpleado = nivel;
                            UsuarioValido = true;
                        }
                        else
                        {
                            UsuarioValido = true;
                        }
                    }
                }
            }

            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
