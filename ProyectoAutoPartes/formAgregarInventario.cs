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

        // Constructor con parámetros para editar productos existentes
        public formAgregarInventario(int id, string nombre, string descripcion, string especificacion, 
                                     double costo, double ganancia, int anio, int stock, string ruta)
        {
            InitializeComponent();
            
            // Asignar valores a los controles del formulario
            ID = id;
            textBoxID.Text = id.ToString();
            textBoxNombreProd.Text = nombre;
            richTextBoxDescripcion.Text = descripcion;
            textBoxEspecificacion.Text = especificacion;
            textBoxCosto.Text = costo.ToString();
            textBoxGanancias.Text = ganancia.ToString();
            textBoxAnioVehiculo.Text = anio.ToString();
            textBoxStock.Text = stock.ToString();
            textBoxRuta.Text = ruta;
            
            // Cambiar el texto del botón para reflejar que estamos editando
            buttonAgregar.Text = "Actualizar";
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            // Validar que los campos requeridos no estén vacíos
            if (string.IsNullOrWhiteSpace(textBoxNombreProd.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del producto.");
                textBoxNombreProd.Focus();
                return;
            }

            // Asignar valores a las propiedades
            Nombre = textBoxNombreProd.Text;
            Descripcion = richTextBoxDescripcion.Text;
            Especificacion = textBoxEspecificacion.Text;

            // Validar que el costo sea un número decimal
            if (double.TryParse(textBoxCosto.Text, out double costo))
            {
                Costo = costo;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Costo.");
                textBoxCosto.Focus();
                return;
            }

            // Validar que la ganancia sea un número decimal
            if (double.TryParse(textBoxGanancias.Text, out double ganancia))
            {
                Ganancia = ganancia;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para la Ganancia.");
                textBoxGanancias.Focus();
                return;
            }

            // Validar que el stock sea un número entero
            if (int.TryParse(textBoxStock.Text, out int stock))
            {
                Stock = stock;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Stock.");
                textBoxStock.Focus();
                return;
            }

            // Validar que el año sea un número entero
            if (int.TryParse(textBoxAnioVehiculo.Text, out int anio))
            {
                Anio = anio;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor válido para el Año del vehículo.");
                textBoxAnioVehiculo.Focus();
                return;
            }

            // Calcular el precio final (costo + ganancia)
            Precio = Costo + Ganancia;

            // Asignar la ruta de la imagen
            Ruta = textBoxRuta.Text;

            // Cerrar el formulario y devolver un resultado exitoso
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void formAgregarInventario_Load(object sender, EventArgs e)
        {
            // Aquí puedes inicializar valores o configuraciones al cargar el formulario
            // Por ejemplo, centrar el formulario en la pantalla
            this.CenterToParent();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            // Confirmar si el usuario desea cancelar la operación
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar? Los datos no guardados se perderán.",
                                                 "Confirmar cancelación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Establecer el resultado como Cancel y cerrar
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            formAniadirImagen formAniadir = new();
            formAniadir.ShowDialog();
            textBoxRuta.Text = formAniadir.RutaImagen;
        }
    }
}