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
    public partial class formAgregarInventario : Form
    {
        // Propiedades públicas de las cuales se obtendran datos en otros formularios
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Especificacion { get; set; }
        public double Costo { get; set; }
        public double Ganancia { get; set; }
        public double Precio { get; set; }
        public int Anio { get; set; }
        public int Stock { get; set; }
        public string Ruta { get; set; }

        public formAgregarInventario()
        {
            InitializeComponent();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            // Asignar los valores a las propiedades, validando los datos ingresados

            // Asignar el valor del nombre
            Nombre = textBoxNombreProd.Text;

            // Asignar el valor de la descripción
            Descripcion = richTextBoxDescripcion.Text;

            //Asignar un valor para la especificacion del vehiculo
            Especificacion = textBoxEspecificacion.Text;

            // Validar que el costo sea un número decimal
            if (double.TryParse(textBoxCosto.Text, out double precio))
            {
                Costo = precio;  // Asignar el valor a la propiedad Precio
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Precio.");
                return;  // Salir si la validación falla
            }

            // Validar que la ganancia sea un número decimal
            if (double.TryParse(textBoxCosto.Text, out double ganancia))
            {
                Ganancia = ganancia;  // Asignar el valor a la propiedad Precio
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Precio.");
                return;  // Salir si la validación falla
            }

            // Validar que el stock sea un número entero
            if (int.TryParse(textBoxStock.Text, out int stock))
            {
                Stock = stock;  // Asignar el valor a la propiedad Stock
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Stock.");
                return;  // Salir si la validación falla
            }

            if (int.TryParse(textBoxAnioVehiculo.Text, out int anio))
            {
                Anio = anio;  // Asignar el valor a la propiedad Stock
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Stock.");
                return;  // Salir si la validación falla
            }

            //Asigna un valor al precio 
            Precio = (Costo + Ganancia);

            //Asignar un valor para la ruta de acceso de la imagen
            Ruta = textBoxRuta.Text;

            // Cerrar el formulario y devolver un resultado exitoso
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void formAgregarInventario_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

        }

        private void formAgregarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si el usuario está intentando cerrar el formulario sin usar el botón Agregar
            if (this.DialogResult != DialogResult.OK && e.CloseReason == CloseReason.UserClosing)
            {
                // Preguntar si realmente desea salir sin guardar
                DialogResult result = MessageBox.Show("¿Está seguro que desea salir sin guardar los datos?",
                                                     "Confirmar salida",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    // Cancelar el cierre del formulario
                    e.Cancel = true;
                }
                else
                {
                    // El usuario confirmó que desea salir sin guardar
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }
    }
}
