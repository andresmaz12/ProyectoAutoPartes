namespace ProyectoAutoPartes
{
    partial class usuarioContrasenia
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
            label20 = new Label();
            textBoxUsuarioEmpleado = new TextBox();
            label1 = new Label();
            textBoxContraseniaEmpleado = new TextBox();
            buttonGuardarUserCont = new Button();
            SuspendLayout();
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(118, 54);
            label20.Name = "label20";
            label20.Size = new Size(47, 15);
            label20.TabIndex = 29;
            label20.Text = "Usuario";
            // 
            // textBoxUsuarioEmpleado
            // 
            textBoxUsuarioEmpleado.Location = new Point(46, 72);
            textBoxUsuarioEmpleado.Name = "textBoxUsuarioEmpleado";
            textBoxUsuarioEmpleado.Size = new Size(229, 23);
            textBoxUsuarioEmpleado.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(107, 98);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 31;
            label1.Text = "Contraseña";
            // 
            // textBoxContraseniaEmpleado
            // 
            textBoxContraseniaEmpleado.Location = new Point(46, 116);
            textBoxContraseniaEmpleado.Name = "textBoxContraseniaEmpleado";
            textBoxContraseniaEmpleado.Size = new Size(229, 23);
            textBoxContraseniaEmpleado.TabIndex = 30;
            // 
            // buttonGuardarUserCont
            // 
            buttonGuardarUserCont.Location = new Point(308, 116);
            buttonGuardarUserCont.Name = "buttonGuardarUserCont";
            buttonGuardarUserCont.Size = new Size(90, 41);
            buttonGuardarUserCont.TabIndex = 32;
            buttonGuardarUserCont.Text = "Agregar Datos";
            buttonGuardarUserCont.UseVisualStyleBackColor = true;
            buttonGuardarUserCont.Click += buttonGuardarUserCont_Click;
            // 
            // usuarioContrasenia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 169);
            Controls.Add(buttonGuardarUserCont);
            Controls.Add(label1);
            Controls.Add(textBoxContraseniaEmpleado);
            Controls.Add(label20);
            Controls.Add(textBoxUsuarioEmpleado);
            Name = "usuarioContrasenia";
            Text = "usuarioContrasenia";
            Load += usuarioContrasenia_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label20;
        private TextBox textBoxUsuarioEmpleado;
        private Label label1;
        private TextBox textBoxContraseniaEmpleado;
        private Button buttonGuardarUserCont;
    }
}