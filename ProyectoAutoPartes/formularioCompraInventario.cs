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
    public partial class formularioCompraInventario : Form
    {
        public string IdProducto { get; set; }
        public int CantComprada {  get; set; }
        public string Proveedor { get; set; }
        public double PrecioUnitario { get; set; }
        public formularioCompraInventario()
        {
            InitializeComponent();
        }

        private void formularioCompraInventario_Load(object sender, EventArgs e)
        {

        }
    }
}
