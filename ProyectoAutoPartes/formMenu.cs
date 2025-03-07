using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlX.XDevAPI.Common;

namespace ProyectoAutoPartes
{
    public partial class formMenu : Form
    {
        private claseGestionInventario inventario;
        private claseGestionVentas ventas;
        private claseClientes clientes;
        private claseGestionRRHH rRHH;

        public formMenu()
        {
            InitializeComponent();
            string connectionString = @"D:\Base de datos VB\ProyectoAutoPartes\Avamce.... de proyecto.prueba6.mwb";
            this.inventario = new claseGestionInventario(connectionString, this);
            this.ventas = new claseGestionVentas(connectionString, this);
            this.clientes = new claseClientes(connectionString, this);
            this.rRHH = new claseGestionRRHH(connectionString, this);
        }

        #region Metodos internos
        public void MoificarEsteticas()
        {
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.FormBorderStyle = FormBorderStyle.None;

        }
        #endregion

        //Modulo inventario
        #region Inventario

        private void buttonAgregarInventario_Click(object sender, EventArgs e)
        {
            formAgregarInventario agregar = new formAgregarInventario();
            agregar.ShowDialog();
            inventario.InsertarDatos(agregar.Nombre, agregar.Descripcion, agregar.Costo, agregar.Ganancia, agregar.Stock);
        }

        private void buttonEditarInventario_Click(object sender, EventArgs e)
        {
            inventario.EditarDatos();
        }

        private void buttonEliminarInventario_Click(object sender, EventArgs e)
        {
            inventario.EliminarDatos();
        }

        private void buttonBuscarInventario_Click(object sender, EventArgs e)
        {
            inventario.BuscarElemento();
        }
        #endregion

        // Modulo de ventas
        #region Ventas

        private void buttonAgregarProducto_Click(object sender, EventArgs e)
        {
            ventas.AgregarProductoLista();
            buttonCancelarVenta.Enabled = true;
            buttonRealizarVenta.Enabled = true;
            buttonEliminarProducto.Enabled = true;
        }

        private void buttonEliminarProducto_Click(object sender, EventArgs e)
        {
            string elemento = Interaction.InputBox("Ingrese el nombre del producto", "Eliminar producto", "Ej. Chazis");
            ventas.EliminiarElemento(elemento);
            Console.WriteLine("Puto si me leen");
        }

        private void buttonRealizarVenta_Click(object sender, EventArgs e)
        {

            buttonCancelarVenta.Enabled = false;
            buttonRealizarVenta.Enabled = false;
            buttonEliminarProducto.Enabled = false;
        }

        private void buttonBusquedaFactura_Click(object sender, EventArgs e)
        {
            string noFactura = Interaction.InputBox("Ingrese el numero de factura", "Busqueda por factura", "Ej. 10XY");
            ventas.BusquedaFactura(noFactura);
        }

        private void buttonCancelarVenta_Click(object sender, EventArgs e)
        {
            DialogResult cancelarVenta = MessageBox.Show("¿Desea cancelar la venta?", "Cancelar venta", MessageBoxButtons.YesNo);
            if (cancelarVenta == DialogResult.Yes)
            {
                ventas.VaciarLista();
            }
            else if (cancelarVenta == DialogResult.No)
            {
                MessageBox.Show("No se cancelara la venta", "Cancelar venta", MessageBoxButtons.OK);
            }
        }
        #endregion

        // Modulo clientes
        #region Clientes

        private void buttonGuardarCliente_Click(object sender, EventArgs e)
        {
            string dpiCliente = textBoxDPI.Text;
            string telefonoCliente = "";
            string nitCliente = textBoxNit.Text;
            string direccionCliente = textBoxDireccion.Text;
            clientes.GuardarCliente(dpiCliente, telefonoCliente, nitCliente, direccionCliente );
        }

        private void buttonBuscarCliente_Click(object sender, EventArgs e)
        {
            clientes.BuscarCliente();
        }
        #endregion

        //Modulo Recursos humanos
        #region RR HH

        private void buttonBuscarEmpleado_Click(object sender, EventArgs e)
        {
            rRHH.BuscarEmpleado();
        }

        private void buttonAgregarEmpleado_Click(object sender, EventArgs e)
        {
            string dpiEmpleado = textBoxDPIEmpleado.Text;
            string nombreEmpleado = textBoxNombreEmpleado.Text;
            string rolEmpleado = comboBoxRol.Text;
            string cuentaBanc = textBoxCuentaBan.Text;

            rRHH.AgregarEmpleado(dpiEmpleado, nombreEmpleado, rolEmpleado, cuentaBanc,);
        }
        #endregion
    }
}
