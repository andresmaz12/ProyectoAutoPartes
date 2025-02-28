namespace ProyectoAutoPartes
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            texBoxUsuario = new TextBox();
            textBoxContrasenia = new TextBox();
            label1 = new Label();
            label2 = new Label();
            buttonIngreso = new Button();
            lableMensajeUsuario = new Label();
            lableMensajeContraseña = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // texBoxUsuario
            // 
            texBoxUsuario.Location = new Point(100, 86);
            texBoxUsuario.Name = "texBoxUsuario";
            texBoxUsuario.Size = new Size(223, 23);
            texBoxUsuario.TabIndex = 0;
            // 
            // textBoxContrasenia
            // 
            textBoxContrasenia.Location = new Point(100, 162);
            textBoxContrasenia.Name = "textBoxContrasenia";
            textBoxContrasenia.Size = new Size(223, 23);
            textBoxContrasenia.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(174, 57);
            label1.Name = "label1";
            label1.Size = new Size(84, 26);
            label1.TabIndex = 2;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(158, 133);
            label2.Name = "label2";
            label2.Size = new Size(118, 26);
            label2.TabIndex = 3;
            label2.Text = "Contraseña";
            // 
            // buttonIngreso
            // 
            buttonIngreso.BackColor = Color.FromArgb(192, 192, 255);
            buttonIngreso.Location = new Point(374, 201);
            buttonIngreso.Name = "buttonIngreso";
            buttonIngreso.Size = new Size(121, 40);
            buttonIngreso.TabIndex = 4;
            buttonIngreso.Text = "Ingresar";
            buttonIngreso.UseVisualStyleBackColor = false;
            buttonIngreso.Click += buttonIngreso_Click;
            // 
            // lableMensajeUsuario
            // 
            lableMensajeUsuario.AutoSize = true;
            lableMensajeUsuario.ForeColor = Color.FromArgb(192, 0, 0);
            lableMensajeUsuario.Location = new Point(141, 112);
            lableMensajeUsuario.Name = "lableMensajeUsuario";
            lableMensajeUsuario.Size = new Size(156, 15);
            lableMensajeUsuario.TabIndex = 5;
            lableMensajeUsuario.Text = "esta label no debe ser visible";
            lableMensajeUsuario.Visible = false;
            // 
            // lableMensajeContraseña
            // 
            lableMensajeContraseña.AutoSize = true;
            lableMensajeContraseña.ForeColor = Color.FromArgb(192, 0, 0);
            lableMensajeContraseña.Location = new Point(140, 188);
            lableMensajeContraseña.Name = "lableMensajeContraseña";
            lableMensajeContraseña.Size = new Size(156, 15);
            lableMensajeContraseña.TabIndex = 6;
            lableMensajeContraseña.Text = "esta label no debe ser visible";
            lableMensajeContraseña.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Sitka Subheading", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(65, 9);
            label3.Name = "label3";
            label3.Size = new Size(305, 42);
            label3.TabIndex = 7;
            label3.Text = "Autopartes \"El Sapo\"";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(507, 253);
            Controls.Add(label3);
            Controls.Add(lableMensajeContraseña);
            Controls.Add(lableMensajeUsuario);
            Controls.Add(buttonIngreso);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxContrasenia);
            Controls.Add(texBoxUsuario);
            Name = "Form1";
            Text = "El sapo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox texBoxUsuario;
        private TextBox textBoxContrasenia;
        private Label label1;
        private Label label2;
        private Button buttonIngreso;
        private Label lableMensajeUsuario;
        private Label lableMensajeContraseña;
        private Label label3;
    }
}
