using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoAutoPartes
{
    public partial class usuarioContrasenia : Form
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }

        public usuarioContrasenia()
        {
            InitializeComponent();
        }

        private void usuarioContrasenia_Load(object sender, EventArgs e)
        {

        }

        private void buttonGuardarUserCont_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuarioEmpleado.Text;
            string contrasenia = textBoxContraseniaEmpleado.Text;
            string pattern1 = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,}$";
            string pattern2 = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$";
            if (Regex.IsMatch(usuario, pattern1))
            {
                Usuario = textBoxUsuarioEmpleado.Text;
                if (Regex.IsMatch(contrasenia, pattern2))
                {
                    Contrasenia = contrasenia.Trim();
                }
                else
                {
                    MessageBox.Show("La contraseña debe contener letras mayusculas, minusculas, al menos un numero, al menos un caracter especial y tener ocho caracteres",
                                    "Error al ingresar datos", MessageBoxButtons.OK);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario debe contener letras mayusculas, minusculas, al menos un numero y tener ocho caracteres",
                                "Error al ingresar datos", MessageBoxButtons.OK);
                Usuario = null;
            }
        }
    }
}
