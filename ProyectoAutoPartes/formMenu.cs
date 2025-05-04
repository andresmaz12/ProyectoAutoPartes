using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using MySqlConnector.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1;
using Mysqlx.Cursor;
using MySqlConnector;

namespace ProyectoAutoPartes
{
    public partial class formMenu : Form
    {
        private claseGestionInventario inventario;
        private claseGestionVentas ventas;
        private claseClientes clientes;
        private claseGestionRRHH rRHH;
        private claseGestionFinanciera financiera;
        private verificarUsuarioContrasenia verificarUsuario;
        private classeProveedores proveedores;

        public formMenu()
        {
            InitializeComponent();
            string connectionString = @"";
            this.inventario = new claseGestionInventario(connectionString, this);
            this.ventas = new claseGestionVentas(connectionString, this);
            this.clientes = new claseClientes(connectionString, this);
            this.rRHH = new claseGestionRRHH(connectionString, this);
            this.financiera = new claseGestionFinanciera(connectionString, this);
            this.verificarUsuario = new verificarUsuarioContrasenia(connectionString, this);
            this.proveedores = new classeProveedores(connectionString, this);
            MoificarEsteticas();
        }

        #region Metodos internos

        public void MoificarEsteticas()
        {
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        //Metodo para que se valide si el usuario tiene permitidio usar la funcion 
        private bool VerificarNivel1()
        {
            verificarUsuarioContrasenia verificar = new verificarUsuarioContrasenia();
            verificar.ShowDialog();
            if (verificar.UsuarioValido == true && verificar.NivelEmpleado <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificarNivel2()
        {
            verificarUsuarioContrasenia verificar = new verificarUsuarioContrasenia();
            verificar.ShowDialog();
            if (verificar.UsuarioValido == true && verificar.NivelEmpleado <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificarNivel3()
        {
            verificarUsuarioContrasenia verificar = new verificarUsuarioContrasenia();
            verificar.ShowDialog();
            if (verificar.UsuarioValido == true && verificar.NivelEmpleado <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificarNivel4()
        {
            verificarUsuarioContrasenia verificar = new verificarUsuarioContrasenia();
            verificar.ShowDialog();
            if (verificar.UsuarioValido == true && verificar.NivelEmpleado <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        //Modulo inventario
        #region Inventario

        private void buttonAgregarInventario_Click(object sender, EventArgs e)
        {
            if (VerificarNivel3() == true)
            {
                formAgregarInventario agregar = new formAgregarInventario();
                agregar.ShowDialog();
                inventario.InsertarDatos(agregar.Nombre, agregar.Descripcion, agregar.Stock, agregar.Especificacion,
                    agregar.Costo, agregar.Ganancia, agregar.Precio, agregar.Anio);
            }
            else
            {
                MessageBox.Show("Error", "No cuenta con el nivel necesario para realizar la accion ");
            }
        }

        private void buttonEditarInventario_Click(object sender, EventArgs e)
        {
            if (VerificarNivel2() == true)
            {
                inventario.EditarDatos();
            }
            else
            {
                MessageBox.Show("Error", "No cuenta con el nivel necesario para realizar la accion ");
            }
        }

        private void buttonEliminarInventario_Click(object sender, EventArgs e)
        {
            if (VerificarNivel2() == true)
            {
                inventario.EliminarDatos();
            }
            else
            {
                MessageBox.Show("Error", "No cuenta con el nivel necesario para realizar la accion ");
            }
        }

        private void buttonBuscarInventario_Click(object sender, EventArgs e)
        {
            inventario.BuscarElemento();
        }

        private void buttonComprarInventario_Click(object sender, EventArgs e)
        {

            if (VerificarNivel1() == true)
            {
                formularioCompraInventario compraInventario = new formularioCompraInventario();
                compraInventario.ShowDialog();
                inventario.ComprarInventario(compraInventario.IdProducto, compraInventario.CantComprada, compraInventario.Proveedor, compraInventario.PrecioUnitario);
            }
            else
            {
                MessageBox.Show("No cuenta con el nivel necesario para realizar la accion ", "Error", MessageBoxButtons.OK);
            }
        }
        #endregion

        // Modulo de ventas
        #region Ventas

        private void buttonAgregarProducto_Click(object sender, EventArgs e)
        {
            //Por medio de esto 
            ventas.AgregarProductoLista();
            //Libera todo los buttons bloqueados para evitar problemas con la lista enlazada
            buttonCancelarVenta.Enabled = true;
            buttonRealizarVenta.Enabled = true;
            buttonEliminarProducto.Enabled = true;
        }

        private void buttonEliminarProducto_Click(object sender, EventArgs e)
        {
            //Por medio del message box se obtiene el ID del producto a elminiar
            string elemento = Interaction.InputBox("Ingrese el ID del producto", "Eliminar producto", "Ej. 00");
            ventas.EliminiarElemento(elemento);
        }

        private void buttonRealizarVenta_Click(object sender, EventArgs e)
        {
            //Se liberan los buttons para evitar problemas con la lista enlazada
            buttonCancelarVenta.Enabled = false;
            buttonRealizarVenta.Enabled = false;
            buttonEliminarProducto.Enabled = false;
        }

        private void buttonBusquedaFactura_Click(object sender, EventArgs e)
        {
            string noFactura = Interaction.InputBox("Ingrese el numero de factura", "Busqueda por factura", "Ej. 10XY");
            ventas.BusquedaFactura(noFactura);
        }

        private void buttonCancelarVenta_Click(object sender, EventArgs e)
        {
            //Por medio de un messageBox se pregunta al dependiente si desea cancelar la venta, lo cual reinica todos los valores en la lista enlazada
            DialogResult cancelarVenta = MessageBox.Show("¿Desea cancelar la venta?", "Cancelar venta", MessageBoxButtons.YesNo);
            if (cancelarVenta == DialogResult.Yes)
            {
                //El metodo simplemente vacia la lista enlazada permitiendo al dependiente hacer otra venta
                ventas.VaciarLista();
                //Se bloquean los botones hasta que se vuelva a agregar otro producto evitando problemas con la lista enlazada
                buttonCancelarVenta.Enabled = true;
                buttonRealizarVenta.Enabled = true;
                buttonEliminarProducto.Enabled = true;
            }
            else if (cancelarVenta == DialogResult.No)
            {
                MessageBox.Show("No se cancelara la venta", "Cancelar venta", MessageBoxButtons.OK);
            }
        }
        private void buttonEditarCompra_Click(object sender, EventArgs e)
        {
            if (VerificarNivel2() == true)
            {
                formEditarVenta editar = new formEditarVenta();
                editar.ShowDialog();
                string ID = editar.IDVenta;
            }

        }
        private void buttonEliminarCompra_Click(object sender, EventArgs e)
        {
            string idVenta = "";
            ventas.EliminarVenta(idVenta);
        }

        private void radioButtonClienteRegNo_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonClienteRegSi.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonClienteRegNo.Checked = false;
            DialogResult registrar = MessageBox.Show("Desea registralo", "Registro de clientes", MessageBoxButtons.YesNoCancel);
            if(registrar == DialogResult.Yes)
            {
                formularioAgregarCliente agregarCliente = new();
                agregarCliente.ShowDialog();
            }
            else if(registrar == DialogResult.No)
            {
                MessageBox.Show("Ok", "Registro de clientes", MessageBoxButtons.OK);
            }
        }
        #endregion

        // Modulo clientes
        #region Clientes

        private void buttonGuardarCliente_Click(object sender, EventArgs e)
        {
            //Datos obtenidos por medio de los textBox en la pestaña/tab "Clientes"
            string dpiCliente = textBoxDPI.Text;
            string telefonoCliente = textBoxTelefonoCliente.Text;
            string nitCliente = textBoxNit.Text;
            string nombreCliente = textBoxNombreCliente.Text;
            string tipoCliente = comboBoxTipoClientes.Text;
            string direccionCliente = textBoxDireccion.Text;
            clientes.GuardarCliente(dpiCliente, nitCliente, nombreCliente, tipoCliente, direccionCliente, 0, telefonoCliente, 0);
        }

        private void buttonBuscarCliente_Click(object sender, EventArgs e)
        {
            string nombre = Interaction.InputBox("Ingrese el nombre", "Busqueda", " ");
            clientes.BuscarCliente(nombre);
        }

        private void buttonEliminiarCliente_Click(object sender, EventArgs e)
        {
            string cliente = Interaction.InputBox("Ingrese el nombre del clinete a eliminar", "Eliminar cliente", "Ej. Claudia Lopez");
            clientes.EliminarClientes(cliente);
        }
        #endregion

        //Modulo Recursos humanos
        #region RR HH
        private void buttonBuscarEmpleado_Click(object sender, EventArgs e)
        {
            //Metodo el cual modifica desde la claseRRHH el datagridviewRRHH para que se muestre al o empleados con el nombre que desea buscar
            rRHH.BuscarEmpleado();
        }

        private void buttonAgregarEmpleado_Click(object sender, EventArgs e)
        {
            //  Llama al formulario usuarioContrasenia para obtener usuario y contraseña
            usuarioContrasenia verificar = new usuarioContrasenia();
            if (verificar.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("No se ingresaron credenciales. Operación cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución si el usuario cancela
            }

            // Obtiene datos de los TextBox en la pestaña "RRHH"
            string dpiEmpleado = textBoxDPIEmpleado.Text.Trim();
            string nombreEmpleado = textBoxNombreEmpleado.Text.Trim();
            string rolEmpleado = comboBoxRol.Text.Trim();
            string cuentaBanc = textBoxCuentaBan.Text.Trim();
            string fechaNac = textBoxFechaNacEmpelado.Text.Trim();

            // Validaciones básicas
            if (string.IsNullOrEmpty(dpiEmpleado) || string.IsNullOrEmpty(nombreEmpleado) ||
                string.IsNullOrEmpty(rolEmpleado) || string.IsNullOrEmpty(cuentaBanc) ||
                string.IsNullOrEmpty(fechaNac))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Declarar la variable antes
            DateTime fecha;

            // Validar formato de fecha
            if (!rRHH.EsFechaValida(fechaNac))
            {
                MessageBox.Show("Formato de fecha incorrecto. Use el formato: yyyy-MM-dd", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detiene la ejecución si la fecha no es válida
            }
            else
            {
                fecha = DateTime.Parse(fechaNac);
            }

            // Obtener usuario y contraseña desde el formulario de verificación
            string usuario = verificar.Usuario;
            string contrasenia = verificar.Contrasenia;

            // Asignar nivel jerárquico según rol
            rRHH.SeleccionarNivel(rolEmpleado);

            // Validar que el salario se obtenga correctamente
            double salario = rRHH.Salario();
            if (salario <= 0)
            {
                MessageBox.Show("El salario debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar empleado
            rRHH.AgregarEmpleado(dpiEmpleado, nombreEmpleado, fecha, rolEmpleado, cuentaBanc, usuario, contrasenia, 0, 0, salario);

            MessageBox.Show("Empleado agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonFiltarSueldoEmpleado_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Ingrese el salario a partir del cual desea filtrar", "Filtrar empleados por rango", "Ej. 3000");
            // Verificar si el usuario canceló
            if (string.IsNullOrEmpty(resultado))
            {
                MessageBox.Show("Operación cancelada por el usuario", "Información");
                return; // Salir del método sin continuar
            }
            // Si llegamos aquí, el usuario presionó OK
            if (int.TryParse(resultado, out int salario))
            {
                if (salario <= 0)
                {
                    MessageBox.Show("No puede haber un salario negativo o igual a cero", "Error");
                }
                else
                {
                    // Obtener los datos filtrados
                    DataTable dtEmpleados = rRHH.FiltrarSalario(salario);

                    // Verificar si se obtuvieron resultados
                    if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                    {
                        // Asignar los datos filtrados al DataGridView
                        dataGridViewRRHH.DataSource = dtEmpleados;

                        // Mostrar mensaje con la cantidad de registros encontrados
                        MessageBox.Show($"Se encontraron {dtEmpleados.Rows.Count} empleados con salario mayor o igual a {salario}", "Resultados");
                    }
                    else
                    {
                        dataGridViewRRHH.DataSource = null;
                        MessageBox.Show($"No se encontraron empleados con salario mayor o igual a {salario}", "Sin resultados");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un valor numérico válido", "Error");
            }
        }

        private void buttonFiltrarRol_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Ingrese el rol a partir del cual desea filtrar", "Filtrar empleados por rol", "Ej. Bodegista");
            // Verificar si el usuario canceló
            if (string.IsNullOrEmpty(resultado))
            {
                MessageBox.Show("Operación cancelada por el usuario", "Información");
                return; // Salir del método sin continuar
            }
            // Si llegamos aquí, el usuario presionó OK
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                // Obtener los datos filtrados por rol
                DataTable dtEmpleados = rRHH.FiltrarPorRol(resultado);

                // Verificar si se obtuvieron resultados
                if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                {
                    // Asignar los datos filtrados al DataGridView
                    dataGridViewRRHH.DataSource = dtEmpleados;

                    // Mostrar mensaje con la cantidad de registros encontrados
                    MessageBox.Show($"Se encontraron {dtEmpleados.Rows.Count} empleados con el rol '{resultado}'", "Resultados");
                }
                else
                {
                    dataGridViewRRHH.DataSource = null;
                    MessageBox.Show($"No se encontraron empleados con el rol '{resultado}'", "Sin resultados");
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un rol válido", "Error");
            }
        }

        private void buttonFiltrarFaltas_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Ingrese el número de faltas a partir del cual desea filtrar", "Filtrar empleados por faltas", "Ej. 3");
            // Verificar si el usuario canceló
            if (string.IsNullOrEmpty(resultado))
            {
                MessageBox.Show("Operación cancelada por el usuario", "Información");
                return; // Salir del método sin continuar
            }
            // Si llegamos aquí, el usuario presionó OK
            if (int.TryParse(resultado, out int faltas))
            {
                if (faltas < 0)
                {
                    MessageBox.Show("No puede haber un número de faltas negativo", "Error");
                }
                else
                {
                    // Obtener los datos filtrados por número de faltas
                    DataTable dtEmpleados = rRHH.FiltrarPorFaltas(faltas);

                    // Verificar si se obtuvieron resultados
                    if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                    {
                        // Asignar los datos filtrados al DataGridView
                        dataGridViewRRHH.DataSource = dtEmpleados;

                        // Mostrar mensaje con la cantidad de registros encontrados
                        MessageBox.Show($"Se encontraron {dtEmpleados.Rows.Count} empleados con {faltas} faltas o más", "Resultados");
                    }
                    else
                    {
                        dataGridViewRRHH.DataSource = null;
                        MessageBox.Show($"No se encontraron empleados con {faltas} faltas o más", "Sin resultados");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un valor numérico válido", "Error");
            }
        }

        private void buttonFiltrarVentas_Click(object sender, EventArgs e)
        {
            string resultado = Interaction.InputBox("Ingrese el número de ventas a partir del cual desea filtrar", "Filtrar empleados por ventas", "Ej. 5");
            // Verificar si el usuario canceló
            if (string.IsNullOrEmpty(resultado))
            {
                MessageBox.Show("Operación cancelada por el usuario", "Información");
                return; // Salir del método sin continuar
            }
            // Si llegamos aquí, el usuario presionó OK
            if (int.TryParse(resultado, out int ventas))
            {
                if (ventas < 0)
                {
                    MessageBox.Show("No puede haber un número de ventas negativo", "Error");
                }
                else
                {
                    // Obtener los datos filtrados por número de ventas
                    DataTable dtEmpleados = rRHH.FiltrarPorVentas(ventas);

                    // Verificar si se obtuvieron resultados
                    if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                    {
                        // Asignar los datos filtrados al DataGridView
                        dataGridViewRRHH.DataSource = dtEmpleados;

                        // Mostrar mensaje con la cantidad de registros encontrados
                        MessageBox.Show($"Se encontraron {dtEmpleados.Rows.Count} empleados con {ventas} ventas o más", "Resultados");
                    }
                    else
                    {
                        dataGridViewRRHH.DataSource = null;
                        MessageBox.Show($"No se encontraron empleados con {ventas} ventas o más", "Sin resultados");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un valor numérico válido", "Error");
            }
        }
        #endregion

        #region CargarDatos en los campos

        // Método para cargar los elementos en el ComboBox desde MySQL
        private void CargarComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection())
                {
                    connection.Open();

                    // Consulta SQL para obtener los datos (ajusta la tabla y el campo según tu base de datos)
                    string query = "SELECT ID, Nombre FROM Productos ORDER BY Nombre";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Configurar el ComboBox
                    comboBoxNombreProd.DisplayMember = "Nombre";  // Campo que se mostrará
                    comboBoxNombreProd.ValueMember = "ID";       // Valor asociado (ID del producto)
                    comboBoxNombreProd.DataSource = dt;           // Asignar origen de datos

                    // Opcionalmente, agregar un elemento inicial
                    // comboBoxProductos.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara cuando cambia la selección en el ComboBox
        private void ComboBoxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar que haya un elemento seleccionado
            if (comboBoxNombreProd.SelectedValue == null)
                return;

            try
            {
                // Obtener el ID del elemento seleccionado
                int productoID = Convert.ToInt32(comboBoxNombreProd.SelectedValue);

                using (MySqlConnection connection = new MySqlConnection())
                {
                    connection.Open();

                    // Consulta para obtener todos los datos del producto seleccionado
                    string query = @"SELECT Nombre, Descripcion, Especificacion, Costo, Ganancia, 
                                Precio, Anio, Stock, Ruta 
                                FROM Productos WHERE ID = @ID";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", productoID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Actualizar los TextBox con los valores obtenidos
                            textBoxID.Text = reader["Nombre"].ToString();
                            textBoxNoFactura.Text = reader["Descripcion"].ToString();
                            textBoxPrecio.Text = reader["Especificacion"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Finanzas
        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int porcentImp = Convert.ToInt32(textBoxImpuestos.Text);
            int donacionesReal = Convert.ToInt32(textBoxDonaciones.Text);
            int ganancias = 0;
            int impuestosTotales = financiera.CalculoImpuestos(porcentImp, ganancias, donacionesReal);
            if (impuestosTotales > 0)
            {
                MessageBox.Show($"Tiene que pagar {impuestosTotales} por sus ingresos de <fecha> a <fecha>");
            }
            else if (impuestosTotales <= 0)
            {
                MessageBox.Show("No tiene que pagar impuestos", "Calculo de impuestos", MessageBoxButtons.OK);
            }
        }


        #endregion

        #region Compras

        private void radioButtonInventario_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonInsumos.Checked = false;
        }


        private void radioButtonInsumos_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonInventario.Checked = false;
        }

        private void radioButtonProveedorSI_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonProveedorNO.Checked = false;
            
        }

        private void radioButtonProveedorNO_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonProveedorSI.Checked = false;

        }
        #endregion

        #region Proveedores
        private void CargarDatos()
        {
            DataTable datos = proveedores.GetTablaDatos();
            dataGridView1.DataSource = datos;
        }
        #endregion


    }
}
