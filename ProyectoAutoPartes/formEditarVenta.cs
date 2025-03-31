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
    public partial class formEditarVenta : Form
    {
        public string IDVenta { get; set; }
        public string NoFactura { get; set; }
        public double PrecioVenta { get; set;}
        public string Fehca {  get; set; }
        public formEditarVenta()
        {
            InitializeComponent();
        }

        private void formEditarVenta_Load(object sender, EventArgs e)
        {

        }
    }
}
