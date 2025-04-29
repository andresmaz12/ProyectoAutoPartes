namespace ProyectoAutoPartes
{
    partial class formAniadirImagen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSeleccionarImagen = new Button();
            pictureBoxImagen = new PictureBox();
            textBoxRuta = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            buttonNo = new Button();
            buttonSi = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImagen).BeginInit();
            SuspendLayout();
            // 
            // buttonSeleccionarImagen
            // 
            buttonSeleccionarImagen.Location = new Point(278, 246);
            buttonSeleccionarImagen.Name = "buttonSeleccionarImagen";
            buttonSeleccionarImagen.Size = new Size(141, 34);
            buttonSeleccionarImagen.TabIndex = 0;
            buttonSeleccionarImagen.Text = "Seleccionar Imagen";
            buttonSeleccionarImagen.UseVisualStyleBackColor = true;
            buttonSeleccionarImagen.Click += buttonSeleccionarImagen_Click;
            // 
            // pictureBoxImagen
            // 
            pictureBoxImagen.Location = new Point(39, 12);
            pictureBoxImagen.Name = "pictureBoxImagen";
            pictureBoxImagen.Size = new Size(218, 212);
            pictureBoxImagen.TabIndex = 1;
            pictureBoxImagen.TabStop = false;
            // 
            // textBoxRuta
            // 
            textBoxRuta.Location = new Point(39, 257);
            textBoxRuta.Name = "textBoxRuta";
            textBoxRuta.Size = new Size(218, 23);
            textBoxRuta.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonNo
            // 
            buttonNo.Location = new Point(357, 190);
            buttonNo.Name = "buttonNo";
            buttonNo.Size = new Size(62, 34);
            buttonNo.TabIndex = 3;
            buttonNo.Text = "No";
            buttonNo.UseVisualStyleBackColor = true;
            buttonNo.Visible = false;
            // 
            // buttonSi
            // 
            buttonSi.Location = new Point(278, 190);
            buttonSi.Name = "buttonSi";
            buttonSi.Size = new Size(60, 34);
            buttonSi.TabIndex = 4;
            buttonSi.Text = "Si";
            buttonSi.UseVisualStyleBackColor = true;
            buttonSi.Visible = false;
            buttonSi.Click += buttonSi_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(263, 172);
            label1.Name = "label1";
            label1.Size = new Size(175, 15);
            label1.TabIndex = 5;
            label1.Text = "¿Esta contento con esa imagen?";
            label1.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(104, 239);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 6;
            label2.Text = "Ruta de la Imagen";
            // 
            // formAniadirImagen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 292);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonSi);
            Controls.Add(buttonNo);
            Controls.Add(textBoxRuta);
            Controls.Add(pictureBoxImagen);
            Controls.Add(buttonSeleccionarImagen);
            Name = "formAniadirImagen";
            Text = "formAniadirImagen";
            ((System.ComponentModel.ISupportInitialize)pictureBoxImagen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSeleccionarImagen;
        private PictureBox pictureBoxImagen;
        private TextBox textBoxRuta;
        private OpenFileDialog openFileDialog1;
        private Button buttonNo;
        private Button buttonSi;
        private Label label1;
        private Label label2;
    }
}