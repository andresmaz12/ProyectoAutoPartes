using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1;
using Mysqlx.Cursor;

namespace ProyectoAutoPartes
{
    public partial class formMenu : Form
    {
        private claseGestionInventario inventario;
        private claseGestionVentas ventas;
        private claseClientes clientes;
        private claseGestionRRHH rRHH;

        public formMenu()
        {
            InitializeComponent();
            string connectionString = @"D:\Base de datos VB\ProyectoAutoPartes\Avamce.... de proyecto.prueba6.mwb";
            this.inventario = new claseGestionInventario(connectionString, this);
            this.ventas = new claseGestionVentas(connectionString, this);
            this.clientes = new claseClientes(connectionString, this);
            this.rRHH = new claseGestionRRHH(connectionString, this);
            MoificarEsteticas();
        }

        #region Metodos internos

        public void MoificarEsteticas()
        {
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        #endregion

        //Modulo inventario
        #region Inventario

        private void buttonAgregarInventario_Click(object sender, EventArgs e)
        {
            formAgregarInventario agregar = new formAgregarInventario();
            agregar.ShowDialog();
            inventario.InsertarDatos(agregar.Nombre, agregar.Descripcion, agregar.Stock, agregar.Especificacion,
                agregar.Costo, agregar.Ganancia, agregar.Precio, agregar.Anio);
        }

        private void buttonEditarInventario_Click(object sender, EventArgs e)
        {
            inventario.EditarDatos();
        }

        private void buttonEliminarInventario_Click(object sender, EventArgs e)
        {
            inventario.EliminarDatos();
        }

        private void buttonBuscarInventario_Click(object sender, EventArgs e)
        {
            inventario.BuscarElemento();
        }

        private void buttonComprarInventario_Click(object sender, EventArgs e)
        {
            inventario.ComprarInventario();
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
            ventas.EditarVenta("", "", "" "");
        }
        private void buttonEliminarCompra_Click(object sender, EventArgs e)
        {
            string idVenta = "";
            ventas.EliminarVenta(idVenta);
        }
        #endregion

        // Modulo clientes
        #region Clientes

        private void buttonGuardarCliente_Click(object sender, EventArgs e)
        {
            //Datos obtenidos por medio de los textBox en la pestaña/tab "Clientes"
            string dpiCliente = textBoxDPI.Text;
            string telefonoCliente = "";
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
    }
    #endregion
}
