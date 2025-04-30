namespace ProyectoAutoPartes
{
    partial class formAgregarInventario
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
            textBoxID = new TextBox();
            textBoxNombreProd = new TextBox();
            textBoxStock = new TextBox();
            textBoxEspecificacion = new TextBox();
            textBoxCosto = new TextBox();
            textBoxGanancias = new TextBox();
            textBoxRuta = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            richTextBoxDescripcion = new RichTextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            buttonAgregar = new Button();
            button1 = new Button();
            label8 = new Label();
            label9 = new Label();
            textBoxAnioVehiculo = new TextBox();
            SuspendLayout();
            // 
            // textBoxID
            // 
            textBoxID.Enabled = false;
            textBoxID.Location = new Point(12, 24);
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new Size(251, 23);
            textBoxID.TabIndex = 0;
            textBoxID.Text = "Se LLenara Automaticamente";
            // 
            // textBoxNombreProd
            // 
            textBoxNombreProd.Location = new Point(12, 68);
            textBoxNombreProd.Name = "textBoxNombreProd";
            textBoxNombreProd.Size = new Size(251, 23);
            textBoxNombreProd.TabIndex = 1;
            // 
            // textBoxStock
            // 
            textBoxStock.Location = new Point(12, 172);
            textBoxStock.Name = "textBoxStock";
            textBoxStock.Size = new Size(251, 23);
            textBoxStock.TabIndex = 3;
            // 
            // textBoxEspecificacion
            // 
            textBoxEspecificacion.Location = new Point(12, 216);
            textBoxEspecificacion.Name = "textBoxEspecificacion";
            textBoxEspecificacion.Size = new Size(251, 23);
            textBoxEspecificacion.TabIndex = 4;
            // 
            // textBoxCosto
            // 
            textBoxCosto.Location = new Point(12, 262);
            textBoxCosto.Name = "textBoxCosto";
            textBoxCosto.Size = new Size(251, 23);
            textBoxCosto.TabIndex = 5;
            // 
            // textBoxGanancias
            // 
            textBoxGanancias.Location = new Point(12, 307);
            textBoxGanancias.Name = "textBoxGanancias";
            textBoxGanancias.Size = new Size(251, 23);
            textBoxGanancias.TabIndex = 6;
            // 
            // textBoxRuta
            // 
            textBoxRuta.Location = new Point(12, 395);
            textBoxRuta.Name = "textBoxRuta";
            textBoxRuta.Size = new Size(251, 23);
            textBoxRuta.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(105, 6);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 8;
            label1.Text = "ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(71, 50);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 9;
            label2.Text = "Nombre del Producto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(71, 94);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 10;
            label3.Text = "Descripcion ";
            // 
            // richTextBoxDescripcion
            // 
            richTextBoxDescripcion.Location = new Point(12, 112);
            richTextBoxDescripcion.Name = "richTextBoxDescripcion";
            richTextBoxDescripcion.Size = new Size(251, 38);
            richTextBoxDescripcion.TabIndex = 11;
            richTextBoxDescripcion.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(71, 154);
            label4.Name = "label4";
            label4.Size = new Size(103, 15);
            label4.TabIndex = 12;
            label4.Text = "Cantidad en Stock";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(71, 198);
            label5.Name = "label5";
            label5.Size = new Size(130, 15);
            label5.TabIndex = 13;
            label5.Text = "Especificacion Vehiculo";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(85, 244);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 14;
            label6.Text = "Costo";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(85, 289);
            label7.Name = "label7";
            label7.Size = new Size(61, 15);
            label7.TabIndex = 15;
            label7.Text = "Ganancias";
            // 
            // buttonAgregar
            // 
            buttonAgregar.Location = new Point(294, 380);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(96, 38);
            buttonAgregar.TabIndex = 16;
            buttonAgregar.Text = "Agregar a Inventario";
            buttonAgregar.UseVisualStyleBackColor = true;
            buttonAgregar.Click += buttonAgregar_Click;
            // 
            // button1
            // 
            button1.Location = new Point(294, 333);
            button1.Name = "button1";
            button1.Size = new Size(96, 38);
            button1.TabIndex = 17;
            button1.Text = "Agregar Imagen";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(71, 377);
            label8.Name = "label8";
            label8.Size = new Size(90, 15);
            label8.TabIndex = 18;
            label8.Text = "Ruta de Imagen";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(84, 333);
            label9.Name = "label9";
            label9.Size = new Size(96, 15);
            label9.TabIndex = 20;
            label9.Text = "Año del vehiculo";
            // 
            // textBoxAnioVehiculo
            // 
            textBoxAnioVehiculo.Location = new Point(11, 350);
            textBoxAnioVehiculo.Name = "textBoxAnioVehiculo";
            textBoxAnioVehiculo.Size = new Size(251, 23);
            textBoxAnioVehiculo.TabIndex = 19;
            // 
            // formAgregarInventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(402, 430);
            Controls.Add(label9);
            Controls.Add(textBoxAnioVehiculo);
            Controls.Add(label8);
            Controls.Add(button1);
            Controls.Add(buttonAgregar);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(richTextBoxDescripcion);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxRuta);
            Controls.Add(textBoxGanancias);
            Controls.Add(textBoxCosto);
            Controls.Add(textBoxEspecificacion);
            Controls.Add(textBoxStock);
            Controls.Add(textBoxNombreProd);
            Controls.Add(textBoxID);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "formAgregarInventario";
            Text = "Agregar al Inventario";
            Load += formAgregarInventario_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxID;
        private TextBox textBoxNombreProd;
        private TextBox textBoxStock;
        private TextBox textBoxEspecificacion;
        private TextBox textBoxCosto;
        private TextBox textBoxGanancias;
        private TextBox textBoxRuta;
        private Label label1;
        private Label label2;
        private Label label3;
        private RichTextBox richTextBoxDescripcion;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button buttonAgregar;
        private Button button1;
        private Label label8;
        private Label label9;
        private TextBox textBoxAnioVehiculo;
    }
}