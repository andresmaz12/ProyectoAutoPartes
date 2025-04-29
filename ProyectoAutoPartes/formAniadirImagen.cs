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
    public partial class formAniadirImagen : Form
    {
        //Obtiene la ruta de la imagen
        public string RutaImagen { get; set; }

        public formAniadirImagen()
        {
            InitializeComponent();
        }

        private void buttonSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Seleccionar imagen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaImagen = openFileDialog.FileName;

                    // Carga la imagen en el PictureBox
                    pictureBoxImagen.Image = Image.FromFile(rutaImagen);
                    // Ajusta el tamaño del PictureBox a la imagen (opcional)
                    pictureBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                    // Mostrar la ruta en un TextBox
                    textBoxRuta.Text = rutaImagen;
                    //Guardar la ruta en el constructor
                    RutaImagen = rutaImagen;
                }
            }
            //Permite al usuario estar de acuerdo con la imagen
            buttonNo.Visible = true;
            buttonSi.Visible = true;
            label1.Visible = true;
        }

        private void buttonSi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
