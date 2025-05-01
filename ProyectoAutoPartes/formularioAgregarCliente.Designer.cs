namespace ProyectoAutoPartes
{
    partial class formularioAgregarCliente
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
            label7 = new Label();
            buttonGuardarCliente = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBoxTipoClientes = new ComboBox();
            textBoxDireccion = new TextBox();
            textBoxNombreCliente = new TextBox();
            textBoxDPI = new TextBox();
            textBoxNit = new TextBox();
            label6 = new Label();
            textBoxTelefonoCliente = new TextBox();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(12, 9);
            label7.Name = "label7";
            label7.Size = new Size(264, 33);
            label7.TabIndex = 27;
            label7.Text = "Agregar Clientes";
            // 
            // buttonGuardarCliente
            // 
            buttonGuardarCliente.Location = new Point(140, 325);
            buttonGuardarCliente.Name = "buttonGuardarCliente";
            buttonGuardarCliente.Size = new Size(133, 40);
            buttonGuardarCliente.TabIndex = 26;
            buttonGuardarCliente.Text = "Guardar cliente";
            buttonGuardarCliente.UseVisualStyleBackColor = true;
            buttonGuardarCliente.Click += buttonGuardarCliente_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(95, 234);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 25;
            label5.Text = "Direccion del Cliente";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(102, 186);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 24;
            label4.Text = "Tipo del Cliente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 142);
            label3.Name = "label3";
            label3.Size = new Size(110, 15);
            label3.TabIndex = 23;
            label3.Text = "Nombre del Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(107, 98);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 22;
            label2.Text = "NIT del Cliente";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(107, 54);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 21;
            label1.Text = "DPI del Cliente";
            // 
            // comboBoxTipoClientes
            // 
            comboBoxTipoClientes.FormattingEnabled = true;
            comboBoxTipoClientes.Items.AddRange(new object[] { "Mayorista", "Cliente comun" });
            comboBoxTipoClientes.Location = new Point(29, 204);
            comboBoxTipoClientes.Name = "comboBoxTipoClientes";
            comboBoxTipoClientes.Size = new Size(229, 23);
            comboBoxTipoClientes.TabIndex = 20;
            // 
            // textBoxDireccion
            // 
            textBoxDireccion.Location = new Point(29, 252);
            textBoxDireccion.Name = "textBoxDireccion";
            textBoxDireccion.Size = new Size(229, 23);
            textBoxDireccion.TabIndex = 19;
            // 
            // textBoxNombreCliente
            // 
            textBoxNombreCliente.Location = new Point(29, 160);
            textBoxNombreCliente.Name = "textBoxNombreCliente";
            textBoxNombreCliente.Size = new Size(229, 23);
            textBoxNombreCliente.TabIndex = 18;
            // 
            // textBoxDPI
            // 
            textBoxDPI.Location = new Point(29, 72);
            textBoxDPI.Name = "textBoxDPI";
            textBoxDPI.Size = new Size(229, 23);
            textBoxDPI.TabIndex = 17;
            // 
            // textBoxNit
            // 
            textBoxNit.Location = new Point(29, 116);
            textBoxNit.Name = "textBoxNit";
            textBoxNit.Size = new Size(229, 23);
            textBoxNit.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(95, 278);
            label6.Name = "label6";
            label6.Size = new Size(111, 15);
            label6.TabIndex = 29;
            label6.Text = "Telefono del Cliente";
            // 
            // textBoxTelefonoCliente
            // 
            textBoxTelefonoCliente.Location = new Point(29, 296);
            textBoxTelefonoCliente.Name = "textBoxTelefonoCliente";
            textBoxTelefonoCliente.Size = new Size(229, 23);
            textBoxTelefonoCliente.TabIndex = 28;
            // 
            // formularioAgregarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(285, 370);
            Controls.Add(label6);
            Controls.Add(textBoxTelefonoCliente);
            Controls.Add(label7);
            Controls.Add(buttonGuardarCliente);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBoxTipoClientes);
            Controls.Add(textBoxDireccion);
            Controls.Add(textBoxNombreCliente);
            Controls.Add(textBoxDPI);
            Controls.Add(textBoxNit);
            Name = "formularioAgregarCliente";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private Button buttonGuardarCliente;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboBoxTipoClientes;
        private TextBox textBoxDireccion;
        private TextBox textBoxNombreCliente;
        private TextBox textBoxDPI;
        private TextBox textBoxNit;
        private Label label6;
        private TextBox textBoxTelefonoCliente;
    }
}