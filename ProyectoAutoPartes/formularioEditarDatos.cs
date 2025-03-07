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
    public partial class formularioEditarDatos : Form
    {
        public formularioEditarDatos()
        {
            InitializeComponent();
        }

        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Precio { get; private set; }
        public int Cantidad { get; private set; }
        public int ID { get; private set; }
        
        private void formularioEditarDatos_Load(object sender, EventArgs e)
        {

        }
    }
}
