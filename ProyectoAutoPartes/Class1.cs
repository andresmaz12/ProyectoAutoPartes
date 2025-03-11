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
            }
        }

        public void EliminarProducto(string elementoBorrar)
        {
            Nodo actual = cabeza;
            int contador = 0;
            while(actual.idProducto != elementoBorrar && actual.siguiente != null)
            {
                actual = actual.siguiente;
                contador++;
            }

            if(contador == tamanio)
            {
                MessageBox.Show("Elemento no presente en la factura o escrito de manera incorrecta", "Remover de factura", MessageBoxButtons.OK);
                return;
            }

            cabeza = cabeza.siguiente;
        }

        public void EfecturarCompra()
        {
            Nodo actual = cabeza;
            
            while (tamanio != 0)
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
                string query = "INSERT INTO Ventas (ID_Producto, NIT, No_Factura, NombreProducto, " +
                              "CantidadLlevada, FechaCompra, PagoIndividual, PagoTotal) " +
                              "VALUES (@ID, @NIT, @NoFactura, @Nombre, @Cantidad, @Fecha, @PagoInd, @PagoTotal)";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@ID", idProducto);
                command.Parameters.AddWithValue("@NIT", nit);
                command.Parameters.AddWithValue("@NoFactura", noFactura);
                command.Parameters.AddWithValue("@Nombre", nombreProducto);
                command.Parameters.AddWithValue("@Cantidad", cantidadLlevada);
                command.Parameters.AddWithValue("@Fecha", fechaCompra);
                command.Parameters.AddWithValue("@PagoInd", pagoIndividual);
                command.Parameters.AddWithValue("@PagoTotal", pagoTotal);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Venta registrada con éxito!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar venta: " + ex.Message);
                }
            }
        }

    }
}
