using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoAutoPartes
{
    public partial class formMenu : Form
    {
        private claseGestionInventario inventario;
        private claseGestionVentas ventas;
        private claseClientes clientes;

        public Form2()
        {
            InitializeComponent();
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Usuario\Downloads\Database5.accdb;";

            // Inicializar las clases de gestión pasando this como referencia
            this.inventario = new claseGestionInventario(connectionString, this);
            this.ventas = new claseGestionVentas(connectionString, this);
            this.clientes = new claseClientes(connectionString, this);
        }

        // Propiedades públicas para acceder a los controles
        public DataGridView InventarioGridView => dataGridView1;
        public DataGridView VentasGridView => dataGridViewVentas;
        public DataGridView ClientesGridView => dataGridViewClientes;

        private async void Form2_Load(object sender, EventArgs e)
        {
            //El metod cargara los datos en el primer dataGriedView
            inventario.CargarDatos();
            ventas.CargarDatosXFecha(dateTimePickerVentas.Value);
            ventas.CargarProductosEnComboBox(comboBoxProducto);
            clientes.CargarDatos();
        }

        private void buttonAgregarDatos_Click(object sender, EventArgs e)
        {

        }
    }
