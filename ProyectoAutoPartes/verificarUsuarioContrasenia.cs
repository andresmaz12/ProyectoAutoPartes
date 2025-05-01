using System;
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
    public partial class verificarUsuarioContrasenia : Form
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public bool UsuarioValido { get; set; }
        public int NivelEmpleado { get; set; }

        public verificarUsuarioContrasenia()
        {
            InitializeComponent();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingrese un valor valido para el usuario", "Error", MessageBoxButtons.OK);
            }
            else
            {
                Usuario = textBox1.Text;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Ingrese un valor valido para el usuario", "Error", MessageBoxButtons.OK);
            }
            else
            {
                Contrasenia = textBox2.Text;
            }
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
