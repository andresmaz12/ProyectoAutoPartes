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
    public partial class formularioAgregarCliente : Form
    {
        public string DPI { get; set; }
        public string NIT { get; set; }
        public string NombreCliente { get; set; }
        public string TipoCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string TelefonoCliente {  get; set; }

        public formularioAgregarCliente()
        {
            InitializeComponent();
        }

        private void buttonGuardarCliente_Click(object sender, EventArgs e)
        {
            // Reiniciamos el contador y validamos campos
            int contador = 0;
            bool camposCompletos = true;
            string mensajeError = "Por favor complete los siguientes campos:\n";

            // Validamos cada campo
            if (string.IsNullOrEmpty(textBoxDPI.Text))
            {
                camposCompletos = false;
                mensajeError += "- DPI\n";
            }
            else
            {
                DPI = textBoxDPI.Text;
                contador++;
            }

            if (string.IsNullOrEmpty(textBoxTelefonoCliente.Text))
            {
                camposCompletos = false;
                mensajeError += "- Teléfono\n";
            }
            else
            {
                TelefonoCliente = textBoxTelefonoCliente.Text;
                contador++;
            }

            if (string.IsNullOrEmpty(textBoxNit.Text))
            {
                camposCompletos = false;
                mensajeError += "- NIT\n";
            }
            else
            {
                NIT = textBoxNit.Text;
                contador++;
            }

            if (string.IsNullOrEmpty(textBoxNombreCliente.Text))
            {
                camposCompletos = false;
                mensajeError += "- Nombre\n";
            }
            else
            {
                NombreCliente = textBoxNombreCliente.Text;
                contador++;
            }

            if (string.IsNullOrEmpty(comboBoxTipoClientes.Text))
            {
                camposCompletos = false;
                mensajeError += "- Tipo de cliente\n";
            }
            else
            {
                TipoCliente = comboBoxTipoClientes.Text;
                contador++;
            }

            if (string.IsNullOrEmpty(textBoxDireccion.Text))
            {
                camposCompletos = false;
                mensajeError += "- Dirección\n";
            }
            else
            {
                DireccionCliente = textBoxDireccion.Text;
                contador++;
            }

            // Si faltan campos, mostramos el error
            if (!camposCompletos)
            {
                MessageBox.Show(mensajeError, "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salimos del método sin continuar
            }

            // Si todos los campos están completos (contador == 6), preguntamos confirmación
            if (contador == 6)
            {
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea agregar este cliente?",
                    "Confirmar agregar cliente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Aquí el código para guardar el cliente
                    MessageBox.Show("Cliente agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Operación cancelada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
