using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ProyectoAutoPartes
{
    public class Nodo
    {
        public string idProducto;
        public string nombreProducto;
        public string nitCliente;
        public int cantidadLlevada;
        public string noFactura;
        public DateTime fechaCompra; // Cambiar de string a DateTime
        public double pagoIndividual;
        public double pagoTotal;
        public Nodo siguiente;

        public Nodo(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, DateTime fechacompra, 
                    double pagoindividual, double pagototal)
        {
            idProducto = idproducto;
            nombreProducto = nombreproducto;
            nitCliente = nitcliente;
            cantidadLlevada = cantidadllevada;
            noFactura = nofactura;
            fechaCompra = fechacompra;
            pagoIndividual = pagoindividual;
            pagoTotal = pagototal;
            siguiente = null;
        }
    }

    public class linkedListFacturas
    {
        private string connectionString = "Server=localHost; ";
        private Nodo cabeza;
        private int tamanio = 0;
        public linkedListFacturas()
        {
            cabeza = null;
        }
        public void AgregarDatosFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, DateTime fechacompra, double pagoindividual, double pagototal)
        {
            Nodo nuevoNodo = new(idproducto, nombreproducto, nitcliente, cantidadllevada, nofactura, fechacompra, pagoindividual, pagototal);
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                tamanio += 1;

            }
            else
            {
                Nodo actual = cabeza;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = nuevoNodo;
                tamanio += 1; // Incrementamos el tamaño aquí también
            }
        }
        public void EliminarProducto(string elementoBorrar)
        {
            if (cabeza == null)
                return;

            // Si el elemento a borrar está en la cabeza
            if (cabeza.idProducto == elementoBorrar)
            {
                cabeza = cabeza.siguiente;
                tamanio--;
                return;
            }

            // Buscar el elemento en el resto de la lista
            Nodo actual = cabeza;
            while (actual.siguiente != null && actual.siguiente.idProducto != elementoBorrar)
            {
                actual = actual.siguiente;
            }

            // Si se encontró el elemento
            if (actual.siguiente != null)
            {
                actual.siguiente = actual.siguiente.siguiente;
                tamanio--;
            }
            else
            {
                MessageBox.Show("Elemento no presente en la factura o escrito de manera incorrecta", "Remover de factura", MessageBoxButtons.OK);
            }
        }
        public void EfecturarCompra()
        {
            Nodo actual = cabeza;

            while (actual != null && tamanio > 0)
            {
                AgregarVenta(actual.idProducto, actual.nitCliente, actual.noFactura, actual.nombreProducto, actual.cantidadLlevada,
                    actual.fechaCompra, actual.pagoIndividual, actual.pagoTotal);
                actual = actual.siguiente;
                tamanio--;
            }
            VaciarLista();
        }
        public void VaciarLista()
        {
            cabeza = null;  // Eliminamos la referencia a la cabeza
            tamanio = 0;    // Restablecemos el tamaño a 0
        }
        private void AgregarVenta(string idProducto, string nit, string noFactura, string nombreProducto,
                           int cantidadLlevada, DateTime fechaCompra, double pagoIndividual, double pagoTotal)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar la venta en la tabla Ventas
                    string insertVentaQuery = "INSERT INTO Ventas (NoFactura, PrecioTotal, Fecha) " +
                                              "VALUES (@NoFactura, @PrecioTotal, @Fecha)";
                    using (MySqlCommand cmdVenta = new MySqlCommand(insertVentaQuery, connection, transaction))
                    {
                        cmdVenta.Parameters.AddWithValue("@NoFactura", noFactura);
                        cmdVenta.Parameters.AddWithValue("@PrecioTotal", pagoTotal);
                        cmdVenta.Parameters.AddWithValue("@Fecha", fechaCompra);
                        cmdVenta.ExecuteNonQuery();
                    }

                    // Descontar del inventario
                    string updateInventarioQuery = "UPDATE Inventario " +
                                                   "SET CantidadEnStock = CantidadEnStock - @Cantidad " +
                                                   "WHERE ID_Producto = @ID_Producto AND CantidadEnStock >= @Cantidad";
                    using (MySqlCommand cmdInventario = new MySqlCommand(updateInventarioQuery, connection, transaction))
                    {
                        cmdInventario.Parameters.AddWithValue("@Cantidad", cantidadLlevada);
                        cmdInventario.Parameters.AddWithValue("@ID_Producto", idProducto);

                        int filasAfectadas = cmdInventario.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            throw new Exception("Stock insuficiente para realizar la venta.");
                        }
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                    MessageBox.Show("Venta registrada y stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al registrar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}

