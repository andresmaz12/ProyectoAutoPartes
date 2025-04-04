namespace ProyectoAutoPartes
{
    partial class formMenu
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            buttonComprarInventario = new Button();
            label8 = new Label();
            buttonBuscarInventario = new Button();
            buttonEliminarInventario = new Button();
            buttonEditarInventario = new Button();
            buttonAgregarInventario = new Button();
            dataGridViewInvetario = new DataGridView();
            tabPage2 = new TabPage();
            buttonEditarCompra = new Button();
            buttonEliminarCompra = new Button();
            buttonBusquedaFactura = new Button();
            buttonCancelarVenta = new Button();
            listBox1 = new ListBox();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            comboBoxNombreProd = new ComboBox();
            numericUpDownCantidad = new NumericUpDown();
            textBoxPrecio = new TextBox();
            textBoxNoFactura = new TextBox();
            textBoxID = new TextBox();
            buttonRealizarVenta = new Button();
            buttonEliminarProducto = new Button();
            buttonAgregarProducto = new Button();
            label10 = new Label();
            label9 = new Label();
            dateTimePickerVentas = new DateTimePicker();
            dataGridViewVentas = new DataGridView();
            tabPage3 = new TabPage();
            buttonEliminiarCliente = new Button();
            label7 = new Label();
            label6 = new Label();
            buttonBuscarCliente = new Button();
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
            dataGridViewClientes = new DataGridView();
            tabPage4 = new TabPage();
            buttonFiltrarVentas = new Button();
            buttonFiltrarFaltas = new Button();
            buttonFiltrarRol = new Button();
            buttonFiltarSueldoEmpleado = new Button();
            buttonEliminarEmpleado = new Button();
            label23 = new Label();
            textBoxTelefonoEmpleado = new TextBox();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            comboBoxRol = new ComboBox();
            textBoxCuentaBan = new TextBox();
            textBoxFechaNacEmpelado = new TextBox();
            textBoxDPIEmpleado = new TextBox();
            textBoxNombreEmpleado = new TextBox();
            buttonAgregarEmpleado = new Button();
            buttonBuscarEmpleado = new Button();
            label12 = new Label();
            dataGridViewRRHH = new DataGridView();
            label11 = new Label();
            tabPage6 = new TabPage();
            button2 = new Button();
            button1 = new Button();
            label30 = new Label();
            label31 = new Label();
            textBoxImpuestos = new TextBox();
            textBoxDonaciones = new TextBox();
            label27 = new Label();
            label28 = new Label();
            label29 = new Label();
            textBoxMultaMora = new TextBox();
            textBoxRenta = new TextBox();
            textBoxServiciosBasicos = new TextBox();
            label26 = new Label();
            dateTimePicker1 = new DateTimePicker();
            buttonCambiarFechaLim = new Button();
            chart1 = new FastReport.DataVisualization.Charting.Chart();
            label25 = new Label();
            label24 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvetario).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewVentas).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRRHH).BeginInit();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Location = new Point(-3, 31);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(982, 422);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.CornflowerBlue;
            tabPage1.Controls.Add(buttonComprarInventario);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(buttonBuscarInventario);
            tabPage1.Controls.Add(buttonEliminarInventario);
            tabPage1.Controls.Add(buttonEditarInventario);
            tabPage1.Controls.Add(buttonAgregarInventario);
            tabPage1.Controls.Add(dataGridViewInvetario);
            tabPage1.Location = new Point(27, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(951, 414);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Inventario";
            // 
            // buttonComprarInventario
            // 
            buttonComprarInventario.Location = new Point(358, 329);
            buttonComprarInventario.Name = "buttonComprarInventario";
            buttonComprarInventario.Size = new Size(113, 49);
            buttonComprarInventario.TabIndex = 16;
            buttonComprarInventario.Text = "Comprar Inventario";
            buttonComprarInventario.UseVisualStyleBackColor = true;
            buttonComprarInventario.Click += buttonComprarInventario_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(280, 11);
            label8.Name = "label8";
            label8.Size = new Size(346, 33);
            label8.TabIndex = 15;
            label8.Text = "Gestion del Inventario";
            // 
            // buttonBuscarInventario
            // 
            buttonBuscarInventario.Location = new Point(801, 329);
            buttonBuscarInventario.Name = "buttonBuscarInventario";
            buttonBuscarInventario.Size = new Size(108, 49);
            buttonBuscarInventario.TabIndex = 4;
            buttonBuscarInventario.Text = "Buscar ";
            buttonBuscarInventario.UseVisualStyleBackColor = true;
            buttonBuscarInventario.Click += buttonBuscarInventario_Click;
            // 
            // buttonEliminarInventario
            // 
            buttonEliminarInventario.Location = new Point(239, 329);
            buttonEliminarInventario.Name = "buttonEliminarInventario";
            buttonEliminarInventario.Size = new Size(113, 49);
            buttonEliminarInventario.TabIndex = 3;
            buttonEliminarInventario.Text = "Eliminar Elemento";
            buttonEliminarInventario.UseVisualStyleBackColor = true;
            buttonEliminarInventario.Click += buttonEliminarInventario_Click;
            // 
            // buttonEditarInventario
            // 
            buttonEditarInventario.Location = new Point(125, 329);
            buttonEditarInventario.Name = "buttonEditarInventario";
            buttonEditarInventario.Size = new Size(108, 49);
            buttonEditarInventario.TabIndex = 2;
            buttonEditarInventario.Text = "Editar Inventario";
            buttonEditarInventario.UseVisualStyleBackColor = true;
            buttonEditarInventario.Click += buttonEditarInventario_Click;
            // 
            // buttonAgregarInventario
            // 
            buttonAgregarInventario.Location = new Point(6, 329);
            buttonAgregarInventario.Name = "buttonAgregarInventario";
            buttonAgregarInventario.Size = new Size(113, 49);
            buttonAgregarInventario.TabIndex = 1;
            buttonAgregarInventario.Text = "Agregar ";
            buttonAgregarInventario.UseVisualStyleBackColor = true;
            buttonAgregarInventario.Click += buttonAgregarInventario_Click;
            // 
            // dataGridViewInvetario
            // 
            dataGridViewInvetario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInvetario.Location = new Point(6, 47);
            dataGridViewInvetario.Name = "dataGridViewInvetario";
            dataGridViewInvetario.Size = new Size(903, 267);
            dataGridViewInvetario.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.CornflowerBlue;
            tabPage2.Controls.Add(buttonEditarCompra);
            tabPage2.Controls.Add(buttonEliminarCompra);
            tabPage2.Controls.Add(buttonBusquedaFactura);
            tabPage2.Controls.Add(buttonCancelarVenta);
            tabPage2.Controls.Add(listBox1);
            tabPage2.Controls.Add(label17);
            tabPage2.Controls.Add(label16);
            tabPage2.Controls.Add(label15);
            tabPage2.Controls.Add(label14);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(comboBoxNombreProd);
            tabPage2.Controls.Add(numericUpDownCantidad);
            tabPage2.Controls.Add(textBoxPrecio);
            tabPage2.Controls.Add(textBoxNoFactura);
            tabPage2.Controls.Add(textBoxID);
            tabPage2.Controls.Add(buttonRealizarVenta);
            tabPage2.Controls.Add(buttonEliminarProducto);
            tabPage2.Controls.Add(buttonAgregarProducto);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(dateTimePickerVentas);
            tabPage2.Controls.Add(dataGridViewVentas);
            tabPage2.Location = new Point(27, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(951, 414);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ventas";
            // 
            // buttonEditarCompra
            // 
            buttonEditarCompra.Location = new Point(559, 315);
            buttonEditarCompra.Name = "buttonEditarCompra";
            buttonEditarCompra.Size = new Size(103, 41);
            buttonEditarCompra.TabIndex = 37;
            buttonEditarCompra.Text = "Editar Compra";
            buttonEditarCompra.UseVisualStyleBackColor = true;
            buttonEditarCompra.Click += buttonEditarCompra_Click;
            // 
            // buttonEliminarCompra
            // 
            buttonEliminarCompra.Location = new Point(668, 315);
            buttonEliminarCompra.Name = "buttonEliminarCompra";
            buttonEliminarCompra.Size = new Size(103, 41);
            buttonEliminarCompra.TabIndex = 36;
            buttonEliminarCompra.Text = "Eliminar Compra";
            buttonEliminarCompra.UseVisualStyleBackColor = true;
            buttonEliminarCompra.Click += buttonEliminarCompra_Click;
            // 
            // buttonBusquedaFactura
            // 
            buttonBusquedaFactura.Location = new Point(777, 315);
            buttonBusquedaFactura.Name = "buttonBusquedaFactura";
            buttonBusquedaFactura.Size = new Size(103, 41);
            buttonBusquedaFactura.TabIndex = 35;
            buttonBusquedaFactura.Text = "Buscar por Factura";
            buttonBusquedaFactura.UseVisualStyleBackColor = true;
            buttonBusquedaFactura.Click += buttonBusquedaFactura_Click;
            // 
            // buttonCancelarVenta
            // 
            buttonCancelarVenta.Location = new Point(357, 367);
            buttonCancelarVenta.Name = "buttonCancelarVenta";
            buttonCancelarVenta.Size = new Size(103, 41);
            buttonCancelarVenta.TabIndex = 34;
            buttonCancelarVenta.Text = "Cancelar Venta";
            buttonCancelarVenta.UseVisualStyleBackColor = true;
            buttonCancelarVenta.Click += buttonCancelarVenta_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(259, 62);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(196, 304);
            listBox1.TabIndex = 33;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(90, 238);
            label17.Name = "label17";
            label17.Size = new Size(95, 15);
            label17.TabIndex = 32;
            label17.Text = "Cantidad llevada";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(90, 189);
            label16.Name = "label16";
            label16.Size = new Size(111, 15);
            label16.TabIndex = 31;
            label16.Text = "Precio del Producto";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(90, 145);
            label15.Name = "label15";
            label15.Size = new Size(122, 15);
            label15.TabIndex = 30;
            label15.Text = "Nombre del Producto";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(107, 100);
            label14.Name = "label14";
            label14.Size = new Size(63, 15);
            label14.TabIndex = 29;
            label14.Text = "No factura";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(90, 56);
            label13.Name = "label13";
            label13.Size = new Size(89, 15);
            label13.TabIndex = 28;
            label13.Text = "ID del Producto";
            // 
            // comboBoxNombreProd
            // 
            comboBoxNombreProd.FormattingEnabled = true;
            comboBoxNombreProd.Location = new Point(33, 163);
            comboBoxNombreProd.Name = "comboBoxNombreProd";
            comboBoxNombreProd.Size = new Size(209, 23);
            comboBoxNombreProd.TabIndex = 27;
            // 
            // numericUpDownCantidad
            // 
            numericUpDownCantidad.Location = new Point(33, 256);
            numericUpDownCantidad.Name = "numericUpDownCantidad";
            numericUpDownCantidad.Size = new Size(209, 23);
            numericUpDownCantidad.TabIndex = 26;
            // 
            // textBoxPrecio
            // 
            textBoxPrecio.Location = new Point(33, 207);
            textBoxPrecio.Name = "textBoxPrecio";
            textBoxPrecio.Size = new Size(209, 23);
            textBoxPrecio.TabIndex = 25;
            textBoxPrecio.Text = "Se llenara solo";
            // 
            // textBoxNoFactura
            // 
            textBoxNoFactura.Enabled = false;
            textBoxNoFactura.Location = new Point(33, 118);
            textBoxNoFactura.Name = "textBoxNoFactura";
            textBoxNoFactura.Size = new Size(209, 23);
            textBoxNoFactura.TabIndex = 23;
            textBoxNoFactura.Text = "Se llenara solo";
            // 
            // textBoxID
            // 
            textBoxID.Location = new Point(33, 74);
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new Size(209, 23);
            textBoxID.TabIndex = 22;
            textBoxID.Text = "Si no sabe el ID seleccione el producto";
            // 
            // buttonRealizarVenta
            // 
            buttonRealizarVenta.Location = new Point(248, 367);
            buttonRealizarVenta.Name = "buttonRealizarVenta";
            buttonRealizarVenta.Size = new Size(103, 41);
            buttonRealizarVenta.TabIndex = 21;
            buttonRealizarVenta.Text = "Realizar Venta";
            buttonRealizarVenta.UseVisualStyleBackColor = true;
            buttonRealizarVenta.Click += buttonRealizarVenta_Click;
            // 
            // buttonEliminarProducto
            // 
            buttonEliminarProducto.Location = new Point(139, 367);
            buttonEliminarProducto.Name = "buttonEliminarProducto";
            buttonEliminarProducto.Size = new Size(103, 41);
            buttonEliminarProducto.TabIndex = 20;
            buttonEliminarProducto.Text = "Eliminar Producto";
            buttonEliminarProducto.UseVisualStyleBackColor = true;
            buttonEliminarProducto.Click += buttonEliminarProducto_Click;
            // 
            // buttonAgregarProducto
            // 
            buttonAgregarProducto.Location = new Point(30, 367);
            buttonAgregarProducto.Name = "buttonAgregarProducto";
            buttonAgregarProducto.Size = new Size(103, 41);
            buttonAgregarProducto.TabIndex = 19;
            buttonAgregarProducto.Text = "Agregar Producto";
            buttonAgregarProducto.UseVisualStyleBackColor = true;
            buttonAgregarProducto.Click += buttonAgregarProducto_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label10.Location = new Point(546, 18);
            label10.Name = "label10";
            label10.Size = new Size(301, 33);
            label10.TabIndex = 16;
            label10.Text = "Historial de Ventas";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label9.Location = new Point(25, 18);
            label9.Name = "label9";
            label9.Size = new Size(360, 33);
            label9.TabIndex = 15;
            label9.Text = "Hacer una venta nueva";
            // 
            // dateTimePickerVentas
            // 
            dateTimePickerVentas.Location = new Point(548, 62);
            dateTimePickerVentas.Name = "dateTimePickerVentas";
            dateTimePickerVentas.Size = new Size(336, 23);
            dateTimePickerVentas.TabIndex = 1;
            // 
            // dataGridViewVentas
            // 
            dataGridViewVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewVentas.Location = new Point(546, 91);
            dataGridViewVentas.Name = "dataGridViewVentas";
            dataGridViewVentas.Size = new Size(338, 218);
            dataGridViewVentas.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.CornflowerBlue;
            tabPage3.Controls.Add(buttonEliminiarCliente);
            tabPage3.Controls.Add(label7);
            tabPage3.Controls.Add(label6);
            tabPage3.Controls.Add(buttonBuscarCliente);
            tabPage3.Controls.Add(buttonGuardarCliente);
            tabPage3.Controls.Add(label5);
            tabPage3.Controls.Add(label4);
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(label2);
            tabPage3.Controls.Add(label1);
            tabPage3.Controls.Add(comboBoxTipoClientes);
            tabPage3.Controls.Add(textBoxDireccion);
            tabPage3.Controls.Add(textBoxNombreCliente);
            tabPage3.Controls.Add(textBoxDPI);
            tabPage3.Controls.Add(textBoxNit);
            tabPage3.Controls.Add(dataGridViewClientes);
            tabPage3.ForeColor = Color.Black;
            tabPage3.Location = new Point(27, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(951, 414);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Clientes";
            // 
            // buttonEliminiarCliente
            // 
            buttonEliminiarCliente.Location = new Point(770, 360);
            buttonEliminiarCliente.Name = "buttonEliminiarCliente";
            buttonEliminiarCliente.Size = new Size(133, 40);
            buttonEliminiarCliente.TabIndex = 16;
            buttonEliminiarCliente.Text = "Eliminar Cliente";
            buttonEliminiarCliente.UseVisualStyleBackColor = true;
            buttonEliminiarCliente.Click += buttonEliminiarCliente_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(639, 16);
            label7.Name = "label7";
            label7.Size = new Size(264, 33);
            label7.TabIndex = 15;
            label7.Text = "Agregar Clientes";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.Location = new Point(111, 16);
            label6.Name = "label6";
            label6.Size = new Size(266, 33);
            label6.TabIndex = 14;
            label6.Text = "Lista de Clientes";
            // 
            // buttonBuscarCliente
            // 
            buttonBuscarCliente.Location = new Point(14, 360);
            buttonBuscarCliente.Name = "buttonBuscarCliente";
            buttonBuscarCliente.Size = new Size(133, 40);
            buttonBuscarCliente.TabIndex = 13;
            buttonBuscarCliente.Text = "Buscar cliente";
            buttonBuscarCliente.UseVisualStyleBackColor = true;
            buttonBuscarCliente.Click += buttonBuscarCliente_Click;
            // 
            // buttonGuardarCliente
            // 
            buttonGuardarCliente.Location = new Point(770, 314);
            buttonGuardarCliente.Name = "buttonGuardarCliente";
            buttonGuardarCliente.Size = new Size(133, 40);
            buttonGuardarCliente.TabIndex = 12;
            buttonGuardarCliente.Text = "Guardar cliente";
            buttonGuardarCliente.UseVisualStyleBackColor = true;
            buttonGuardarCliente.Click += buttonGuardarCliente_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(740, 247);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 11;
            label5.Text = "Direccion del Cliente";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(747, 199);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 10;
            label4.Text = "Tipo del Cliente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(740, 155);
            label3.Name = "label3";
            label3.Size = new Size(110, 15);
            label3.TabIndex = 9;
            label3.Text = "Nombre del Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(752, 111);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 8;
            label2.Text = "NIT del Cliente";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(752, 67);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 7;
            label1.Text = "DPI del Cliente";
            // 
            // comboBoxTipoClientes
            // 
            comboBoxTipoClientes.FormattingEnabled = true;
            comboBoxTipoClientes.Items.AddRange(new object[] { "Mayorista", "Cliente comun" });
            comboBoxTipoClientes.Location = new Point(674, 217);
            comboBoxTipoClientes.Name = "comboBoxTipoClientes";
            comboBoxTipoClientes.Size = new Size(229, 23);
            comboBoxTipoClientes.TabIndex = 6;
            // 
            // textBoxDireccion
            // 
            textBoxDireccion.Location = new Point(674, 265);
            textBoxDireccion.Name = "textBoxDireccion";
            textBoxDireccion.Size = new Size(229, 23);
            textBoxDireccion.TabIndex = 5;
            // 
            // textBoxNombreCliente
            // 
            textBoxNombreCliente.Location = new Point(674, 173);
            textBoxNombreCliente.Name = "textBoxNombreCliente";
            textBoxNombreCliente.Size = new Size(229, 23);
            textBoxNombreCliente.TabIndex = 3;
            // 
            // textBoxDPI
            // 
            textBoxDPI.Location = new Point(674, 85);
            textBoxDPI.Name = "textBoxDPI";
            textBoxDPI.Size = new Size(229, 23);
            textBoxDPI.TabIndex = 2;
            // 
            // textBoxNit
            // 
            textBoxNit.Location = new Point(674, 129);
            textBoxNit.Name = "textBoxNit";
            textBoxNit.Size = new Size(229, 23);
            textBoxNit.TabIndex = 1;
            // 
            // dataGridViewClientes
            // 
            dataGridViewClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientes.Location = new Point(14, 52);
            dataGridViewClientes.Name = "dataGridViewClientes";
            dataGridViewClientes.Size = new Size(516, 302);
            dataGridViewClientes.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.CornflowerBlue;
            tabPage4.Controls.Add(buttonFiltrarVentas);
            tabPage4.Controls.Add(buttonFiltrarFaltas);
            tabPage4.Controls.Add(buttonFiltrarRol);
            tabPage4.Controls.Add(buttonFiltarSueldoEmpleado);
            tabPage4.Controls.Add(buttonEliminarEmpleado);
            tabPage4.Controls.Add(label23);
            tabPage4.Controls.Add(textBoxTelefonoEmpleado);
            tabPage4.Controls.Add(label18);
            tabPage4.Controls.Add(label19);
            tabPage4.Controls.Add(label20);
            tabPage4.Controls.Add(label21);
            tabPage4.Controls.Add(label22);
            tabPage4.Controls.Add(comboBoxRol);
            tabPage4.Controls.Add(textBoxCuentaBan);
            tabPage4.Controls.Add(textBoxFechaNacEmpelado);
            tabPage4.Controls.Add(textBoxDPIEmpleado);
            tabPage4.Controls.Add(textBoxNombreEmpleado);
            tabPage4.Controls.Add(buttonAgregarEmpleado);
            tabPage4.Controls.Add(buttonBuscarEmpleado);
            tabPage4.Controls.Add(label12);
            tabPage4.Controls.Add(dataGridViewRRHH);
            tabPage4.Controls.Add(label11);
            tabPage4.Location = new Point(27, 4);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(951, 414);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "RR HH";
            // 
            // buttonFiltrarVentas
            // 
            buttonFiltrarVentas.Location = new Point(449, 339);
            buttonFiltrarVentas.Name = "buttonFiltrarVentas";
            buttonFiltrarVentas.Size = new Size(103, 41);
            buttonFiltrarVentas.TabIndex = 36;
            buttonFiltrarVentas.Text = "Filtrar por Ventas";
            buttonFiltrarVentas.UseVisualStyleBackColor = true;
            buttonFiltrarVentas.Click += buttonFiltrarVentas_Click;
            // 
            // buttonFiltrarFaltas
            // 
            buttonFiltrarFaltas.Location = new Point(340, 339);
            buttonFiltrarFaltas.Name = "buttonFiltrarFaltas";
            buttonFiltrarFaltas.Size = new Size(103, 41);
            buttonFiltrarFaltas.TabIndex = 35;
            buttonFiltrarFaltas.Text = "Filtrar por Faltas";
            buttonFiltrarFaltas.UseVisualStyleBackColor = true;
            buttonFiltrarFaltas.Click += buttonFiltrarFaltas_Click;
            // 
            // buttonFiltrarRol
            // 
            buttonFiltrarRol.Location = new Point(231, 339);
            buttonFiltrarRol.Name = "buttonFiltrarRol";
            buttonFiltrarRol.Size = new Size(103, 41);
            buttonFiltrarRol.TabIndex = 34;
            buttonFiltrarRol.Text = "Filtrar por Rol ";
            buttonFiltrarRol.UseVisualStyleBackColor = true;
            buttonFiltrarRol.Click += buttonFiltrarRol_Click;
            // 
            // buttonFiltarSueldoEmpleado
            // 
            buttonFiltarSueldoEmpleado.Location = new Point(122, 339);
            buttonFiltarSueldoEmpleado.Name = "buttonFiltarSueldoEmpleado";
            buttonFiltarSueldoEmpleado.Size = new Size(103, 41);
            buttonFiltarSueldoEmpleado.TabIndex = 33;
            buttonFiltarSueldoEmpleado.Text = "Filtrar por Sueldo";
            buttonFiltarSueldoEmpleado.UseVisualStyleBackColor = true;
            buttonFiltarSueldoEmpleado.Click += buttonFiltarSueldoEmpleado_Click;
            // 
            // buttonEliminarEmpleado
            // 
            buttonEliminarEmpleado.Location = new Point(613, 359);
            buttonEliminarEmpleado.Name = "buttonEliminarEmpleado";
            buttonEliminarEmpleado.Size = new Size(103, 41);
            buttonEliminarEmpleado.TabIndex = 32;
            buttonEliminarEmpleado.Text = "Eliminar Empleado";
            buttonEliminarEmpleado.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(642, 302);
            label23.Name = "label23";
            label23.Size = new Size(171, 15);
            label23.TabIndex = 31;
            label23.Text = "Numero de Telefono Empleado";
            // 
            // textBoxTelefonoEmpleado
            // 
            textBoxTelefonoEmpleado.Location = new Point(613, 320);
            textBoxTelefonoEmpleado.Name = "textBoxTelefonoEmpleado";
            textBoxTelefonoEmpleado.Size = new Size(229, 23);
            textBoxTelefonoEmpleado.TabIndex = 30;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(679, 209);
            label18.Name = "label18";
            label18.Size = new Size(93, 15);
            label18.TabIndex = 29;
            label18.Text = "Cuenta Bancaria";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(686, 161);
            label19.Name = "label19";
            label19.Size = new Size(99, 15);
            label19.TabIndex = 28;
            label19.Text = "Rol del Empleado";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(642, 255);
            label20.Name = "label20";
            label20.Size = new Size(175, 15);
            label20.TabIndex = 27;
            label20.Text = "Fecha de Nacimiento Empelado";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(691, 116);
            label21.Name = "label21";
            label21.Size = new Size(126, 15);
            label21.TabIndex = 26;
            label21.Text = "Nombre del Empleado";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(691, 72);
            label22.Name = "label22";
            label22.Size = new Size(100, 15);
            label22.TabIndex = 25;
            label22.Text = "DPI del Empleado";
            // 
            // comboBoxRol
            // 
            comboBoxRol.FormattingEnabled = true;
            comboBoxRol.Items.AddRange(new object[] { "Bodeguista", "Gerente", "Cajero ", "Conserje ", "Vendedor" });
            comboBoxRol.Location = new Point(613, 179);
            comboBoxRol.Name = "comboBoxRol";
            comboBoxRol.Size = new Size(229, 23);
            comboBoxRol.TabIndex = 24;
            // 
            // textBoxCuentaBan
            // 
            textBoxCuentaBan.Location = new Point(613, 227);
            textBoxCuentaBan.Name = "textBoxCuentaBan";
            textBoxCuentaBan.Size = new Size(229, 23);
            textBoxCuentaBan.TabIndex = 23;
            // 
            // textBoxFechaNacEmpelado
            // 
            textBoxFechaNacEmpelado.Location = new Point(613, 273);
            textBoxFechaNacEmpelado.Name = "textBoxFechaNacEmpelado";
            textBoxFechaNacEmpelado.Size = new Size(229, 23);
            textBoxFechaNacEmpelado.TabIndex = 22;
            // 
            // textBoxDPIEmpleado
            // 
            textBoxDPIEmpleado.Location = new Point(613, 90);
            textBoxDPIEmpleado.Name = "textBoxDPIEmpleado";
            textBoxDPIEmpleado.Size = new Size(229, 23);
            textBoxDPIEmpleado.TabIndex = 21;
            // 
            // textBoxNombreEmpleado
            // 
            textBoxNombreEmpleado.Location = new Point(613, 134);
            textBoxNombreEmpleado.Name = "textBoxNombreEmpleado";
            textBoxNombreEmpleado.Size = new Size(229, 23);
            textBoxNombreEmpleado.TabIndex = 20;
            // 
            // buttonAgregarEmpleado
            // 
            buttonAgregarEmpleado.Location = new Point(739, 359);
            buttonAgregarEmpleado.Name = "buttonAgregarEmpleado";
            buttonAgregarEmpleado.Size = new Size(103, 41);
            buttonAgregarEmpleado.TabIndex = 19;
            buttonAgregarEmpleado.Text = "Agregar Empleado";
            buttonAgregarEmpleado.UseVisualStyleBackColor = true;
            buttonAgregarEmpleado.Click += buttonAgregarEmpleado_Click;
            // 
            // buttonBuscarEmpleado
            // 
            buttonBuscarEmpleado.Location = new Point(13, 339);
            buttonBuscarEmpleado.Name = "buttonBuscarEmpleado";
            buttonBuscarEmpleado.Size = new Size(103, 41);
            buttonBuscarEmpleado.TabIndex = 18;
            buttonBuscarEmpleado.Text = "Buscar Empleao";
            buttonBuscarEmpleado.UseVisualStyleBackColor = true;
            buttonBuscarEmpleado.Click += buttonBuscarEmpleado_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label12.Location = new Point(574, 36);
            label12.Name = "label12";
            label12.Size = new Size(280, 33);
            label12.TabIndex = 17;
            label12.Text = "Agregar Empleado";
            // 
            // dataGridViewRRHH
            // 
            dataGridViewRRHH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRRHH.Location = new Point(13, 72);
            dataGridViewRRHH.Name = "dataGridViewRRHH";
            dataGridViewRRHH.Size = new Size(539, 261);
            dataGridViewRRHH.TabIndex = 16;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label11.Location = new Point(144, 36);
            label11.Name = "label11";
            label11.Size = new Size(299, 33);
            label11.TabIndex = 15;
            label11.Text = "Lista de empleados";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = Color.CornflowerBlue;
            tabPage6.Controls.Add(button2);
            tabPage6.Controls.Add(button1);
            tabPage6.Controls.Add(label30);
            tabPage6.Controls.Add(label31);
            tabPage6.Controls.Add(textBoxImpuestos);
            tabPage6.Controls.Add(textBoxDonaciones);
            tabPage6.Controls.Add(label27);
            tabPage6.Controls.Add(label28);
            tabPage6.Controls.Add(label29);
            tabPage6.Controls.Add(textBoxMultaMora);
            tabPage6.Controls.Add(textBoxRenta);
            tabPage6.Controls.Add(textBoxServiciosBasicos);
            tabPage6.Controls.Add(label26);
            tabPage6.Controls.Add(dateTimePicker1);
            tabPage6.Controls.Add(buttonCambiarFechaLim);
            tabPage6.Controls.Add(chart1);
            tabPage6.Controls.Add(label25);
            tabPage6.Controls.Add(label24);
            tabPage6.Location = new Point(27, 4);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(951, 414);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Finanzas";
            // 
            // button2
            // 
            button2.Location = new Point(639, 194);
            button2.Name = "button2";
            button2.Size = new Size(121, 33);
            button2.TabIndex = 33;
            button2.Text = "Cambiar Valores";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(747, 367);
            button1.Name = "button1";
            button1.Size = new Size(121, 33);
            button1.TabIndex = 32;
            button1.Text = "Calcular";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(690, 323);
            label30.Name = "label30";
            label30.Size = new Size(123, 15);
            label30.TabIndex = 31;
            label30.Text = "Donaciones realizadas";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(690, 279);
            label31.Name = "label31";
            label31.Size = new Size(140, 15);
            label31.TabIndex = 30;
            label31.Text = "Porcentaje de Impuestos ";
            // 
            // textBoxImpuestos
            // 
            textBoxImpuestos.Location = new Point(639, 297);
            textBoxImpuestos.Name = "textBoxImpuestos";
            textBoxImpuestos.Size = new Size(229, 23);
            textBoxImpuestos.TabIndex = 29;
            // 
            // textBoxDonaciones
            // 
            textBoxDonaciones.Location = new Point(639, 341);
            textBoxDonaciones.Name = "textBoxDonaciones";
            textBoxDonaciones.Size = new Size(229, 23);
            textBoxDonaciones.TabIndex = 28;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(690, 147);
            label27.Name = "label27";
            label27.Size = new Size(150, 15);
            label27.TabIndex = 27;
            label27.Text = "Multas o Moras Pendientes";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(690, 103);
            label28.Name = "label28";
            label28.Size = new Size(123, 15);
            label28.TabIndex = 26;
            label28.Text = "Total Servicios Basicos";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(705, 59);
            label29.Name = "label29";
            label29.Size = new Size(96, 15);
            label29.TabIndex = 25;
            label29.Text = "Costo de la renta";
            // 
            // textBoxMultaMora
            // 
            textBoxMultaMora.Location = new Point(639, 165);
            textBoxMultaMora.Name = "textBoxMultaMora";
            textBoxMultaMora.Size = new Size(229, 23);
            textBoxMultaMora.TabIndex = 24;
            // 
            // textBoxRenta
            // 
            textBoxRenta.Location = new Point(639, 77);
            textBoxRenta.Name = "textBoxRenta";
            textBoxRenta.Size = new Size(229, 23);
            textBoxRenta.TabIndex = 23;
            // 
            // textBoxServiciosBasicos
            // 
            textBoxServiciosBasicos.Location = new Point(639, 121);
            textBoxServiciosBasicos.Name = "textBoxServiciosBasicos";
            textBoxServiciosBasicos.Size = new Size(229, 23);
            textBoxServiciosBasicos.TabIndex = 22;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label26.Location = new Point(610, 26);
            label26.Name = "label26";
            label26.Size = new Size(293, 33);
            label26.TabIndex = 21;
            label26.Text = "Gastos Adicionales";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(18, 79);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(214, 23);
            dateTimePicker1.TabIndex = 20;
            // 
            // buttonCambiarFechaLim
            // 
            buttonCambiarFechaLim.Location = new Point(14, 367);
            buttonCambiarFechaLim.Name = "buttonCambiarFechaLim";
            buttonCambiarFechaLim.Size = new Size(140, 33);
            buttonCambiarFechaLim.TabIndex = 19;
            buttonCambiarFechaLim.Text = "Cambiar Fecha Limite";
            buttonCambiarFechaLim.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chart1.Location = new Point(14, 108);
            chart1.Name = "chart1";
            chart1.Size = new Size(416, 253);
            chart1.TabIndex = 18;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label25.Location = new Point(610, 243);
            label25.Name = "label25";
            label25.Size = new Size(327, 33);
            label25.TabIndex = 17;
            label25.Text = "Calculo de impuestos";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Pivot Classic", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label24.Location = new Point(94, 26);
            label24.Name = "label24";
            label24.Size = new Size(141, 33);
            label24.TabIndex = 16;
            label24.Text = "Finanzas";
            // 
            // formMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LawnGreen;
            ClientSize = new Size(978, 447);
            Controls.Add(tabControl1);
            MaximizeBox = false;
            Name = "formMenu";
            Text = "formMenu";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInvetario).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewVentas).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRRHH).EndInit();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button buttonEditarInventario;
        private Button buttonAgregarInventario;
        private Button buttonBuscarInventario;
        private Button buttonEliminarInventario;
        private TabPage tabPage6;
        public DateTimePicker dateTimePickerVentas;
        public DataGridView dataGridViewVentas;
        public DataGridView dataGridViewClientes;
        private ComboBox comboBoxTipoClientes;
        private TextBox textBoxDireccion;
        private TextBox textBoxNombreCliente;
        private TextBox textBoxDPI;
        private TextBox textBoxNit;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button buttonGuardarCliente;
        private Label label8;
        private Label label10;
        private Label label9;
        private Label label7;
        private Label label6;
        private Button buttonBuscarCliente;
        private Button buttonAgregarProducto;
        private Button buttonAgregarEmpleado;
        private Button buttonBuscarEmpleado;
        private Label label12;
        private DataGridView dataGridViewRRHH;
        private Label label11;
        private ComboBox comboBoxNombreProd;
        private NumericUpDown numericUpDownCantidad;
        private TextBox textBoxPrecio;
        private TextBox textBoxNoFactura;
        private TextBox textBoxID;
        private Button buttonRealizarVenta;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label17;
        private Label label16;
        private Button buttonCancelarVenta;
        private ListBox listBox1;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private ComboBox comboBoxRol;
        private TextBox textBoxCuentaBan;
        private TextBox textBoxFechaNacEmpelado;
        private TextBox textBoxDPIEmpleado;
        private TextBox textBoxNombreEmpleado;
        private Button buttonBusquedaFactura;
        public Button buttonEliminarProducto;
        public DataGridView dataGridViewInvetario;
        private Label label23;
        private TextBox textBoxTelefonoEmpleado;
        private Button buttonEliminiarCliente;
        private Button buttonEditarCompra;
        private Button buttonEliminarCompra;
        private Button buttonFiltrarFaltas;
        private Button buttonFiltrarRol;
        private Button buttonFiltarSueldoEmpleado;
        private Button buttonEliminarEmpleado;
        private Button buttonFiltrarVentas;
        private Button buttonComprarInventario;
        private Label label25;
        private Label label24;
        private Button buttonCambiarFechaLim;
        private FastReport.DataVisualization.Charting.Chart chart1;
        private Label label26;
        private DateTimePicker dateTimePicker1;
        private Button button2;
        private Button button1;
        private Label label30;
        private Label label31;
        private TextBox textBoxImpuestos;
        private TextBox textBoxDonaciones;
        private Label label27;
        private Label label28;
        private Label label29;
        private TextBox textBoxMultaMora;
        private TextBox textBoxRenta;
        private TextBox textBoxServiciosBasicos;
    }
}