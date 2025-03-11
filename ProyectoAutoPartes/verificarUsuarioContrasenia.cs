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

        public verificarUsuarioContrasenia()
        {
            InitializeComponent();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            Usuario = textBox1.Text;
            Contrasenia = textBox2.Text;
        }
    }
}
