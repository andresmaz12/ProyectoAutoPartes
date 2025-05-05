using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProyectoAutoPartes;


namespace ProyectoAutoPartes
{
    public class NodoVenta
    {
        public string idProducto;
        public string nombreProducto;
        public string nitCliente;
        public int cantidadLlevada;
        public string noFactura;
        public DateTime fechaVenta;
        public double pagoIndividual;
        public double pagoTotal;
        public NodoVenta siguiente;

        public NodoVenta(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada,
                        string nofactura, DateTime fechaventa, double pagoindividual, double pagototal)
        {
            idProducto = idproducto;
            nombreProducto = nombreproducto;
            nitCliente = nitcliente;
            cantidadLlevada = cantidadllevada;
            noFactura = nofactura;
            fechaVenta = fechaventa;
            pagoIndividual = pagoindividual;
            pagoTotal = pagototal;
            siguiente = null;
        }
    }

    public class NodoCompra
    {
        public string idProducto;
        public string nombreProducto;
        public string nitProveedor;
        public int cantidadComprada;
        public string noFactura;
        public DateTime fechaCompra;
        public double costoUnitario;
        public double costoTotal;
        public NodoCompra siguiente;

        public NodoCompra(string idproducto, string nombreproducto, string nitproveedor, int cantidadcomprada,
                         string nofactura, DateTime fechacompra, double costounitario, double costototal)
        {
            idProducto = idproducto;
            nombreProducto = nombreproducto;
            nitProveedor = nitproveedor;
            cantidadComprada = cantidadcomprada;
            noFactura = nofactura;
            fechaCompra = fechacompra;
            costoUnitario = costounitario;
            costoTotal = costototal;
            siguiente = null;
        }
    }

    public class LinkedListVentas
    {
        private string connectionString = "Server=localHost;Database=tu_basedatos;Uid=usuario;Pwd=contraseña;";
        private NodoVenta cabezaVenta;
        private int cantidadVentas = 0;

        public int ObtenerCantidadVentas() => cantidadVentas;
        private int tamanioVentas = 0;

        public LinkedListVentas()
        {
            cabezaVenta = null;
        }

        public void AgregarVenta(string idproducto, string nombreproducto, string nitcliente,
                                int cantidadllevada, string nofactura, DateTime fechaventa,
                                double pagoindividual, double pagototal)
        {
            NodoVenta nuevoNodo = new NodoVenta(idproducto, nombreproducto, nitcliente, cantidadllevada,
                                               nofactura, fechaventa, pagoindividual, pagototal);

            if (cabezaVenta == null)
            {
                cabezaVenta = nuevoNodo;
            }
            else
            {
                NodoVenta actual = cabezaVenta;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = nuevoNodo;
            }
            tamanioVentas++;
        }

        public void EliminarVenta(string idProducto)
        {
            if (cabezaVenta == null) return;

            if (cabezaVenta.idProducto == idProducto)
            {
                cabezaVenta = cabezaVenta.siguiente;
                tamanioVentas--;
                return;
            }

            NodoVenta actual = cabezaVenta;
            while (actual.siguiente != null && actual.siguiente.idProducto != idProducto)
            {
                actual = actual.siguiente;
            }

            if (actual.siguiente != null)
            {
                actual.siguiente = actual.siguiente.siguiente;
                tamanioVentas--;
            }
        }

        public void ProcesarVentas()
        {
            NodoVenta actual = cabezaVenta;

            while (actual != null)
            {
                RegistrarVentaEnBD(actual);
                actual = actual.siguiente;
            }
            VaciarListaVentas();
        }

        private void RegistrarVentaEnBD(NodoVenta venta)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar en tabla Ventas
                    string queryVenta = "INSERT INTO Ventas (NoFactura, ID_Producto, NIT_Cliente, Cantidad, " +
                                        "PrecioUnitario, PrecioTotal, Fecha) VALUES (@NoFactura, @IDProducto, " +
                                        "@NITCliente, @Cantidad, @PrecioUnitario, @PrecioTotal, @Fecha)";

                    using (MySqlCommand cmd = new MySqlCommand(queryVenta, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@NoFactura", venta.noFactura);
                        cmd.Parameters.AddWithValue("@IDProducto", venta.idProducto);
                        cmd.Parameters.AddWithValue("@NITCliente", venta.nitCliente);
                        cmd.Parameters.AddWithValue("@Cantidad", venta.cantidadLlevada);
                        cmd.Parameters.AddWithValue("@PrecioUnitario", venta.pagoIndividual);
                        cmd.Parameters.AddWithValue("@PrecioTotal", venta.pagoTotal);
                        cmd.Parameters.AddWithValue("@Fecha", venta.fechaVenta);
                        cmd.ExecuteNonQuery();
                    }

                    // Actualizar inventario
                    string queryInventario = "UPDATE Inventario SET Stock = Stock - @Cantidad " +
                                           "WHERE ID_Producto = @IDProducto AND Stock >= @Cantidad";

                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IDProducto", venta.idProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", venta.cantidadLlevada);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows == 0)
                        {
                            throw new Exception("No hay suficiente stock para el producto: " + venta.nombreProducto);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al procesar venta: " + ex.Message);
                }
            }
        }

        public void VaciarListaVentas()
        {
            cabezaVenta = null;
            tamanioVentas = 0;
        }
    }

    public class LinkedListCompras
    {
        private string connectionString = "Server=localHost;Database=tu_basedatos;Uid=usuario;Pwd=contraseña;";
        private NodoCompra cabezaCompra;
        private int tamanioCompras = 0;

        public LinkedListCompras()
        {
            cabezaCompra = null;
        }

        public void AgregarCompra(string idproducto, string nombreproducto, string nitproveedor,
                                int cantidadcomprada, string nofactura, DateTime fechacompra,
                                double costounitario, double costototal)
        {
            NodoCompra nuevoNodo = new NodoCompra(idproducto, nombreproducto, nitproveedor, cantidadcomprada,
                                                 nofactura, fechacompra, costounitario, costototal);

            if (cabezaCompra == null)
            {
                cabezaCompra = nuevoNodo;
            }
            else
            {
                NodoCompra actual = cabezaCompra;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = nuevoNodo;
            }
            tamanioCompras++;
        }

        public void EliminarCompra(string idProducto)
        {
            if (cabezaCompra == null) return;

            if (cabezaCompra.idProducto == idProducto)
            {
                cabezaCompra = cabezaCompra.siguiente;
                tamanioCompras--;
                return;
            }

            NodoCompra actual = cabezaCompra;
            while (actual.siguiente != null && actual.siguiente.idProducto != idProducto)
            {
                actual = actual.siguiente;
            }

            if (actual.siguiente != null)
            {
                actual.siguiente = actual.siguiente.siguiente;
                tamanioCompras--;
            }
        }

        public void ProcesarCompras()
        {
            NodoCompra actual = cabezaCompra;

            while (actual != null)
            {
                RegistrarCompraEnBD(actual);
                actual = actual.siguiente;
            }
            VaciarListaCompras();
        }

        private void RegistrarCompraEnBD(NodoCompra compra)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar en tabla Compras
                    string queryCompra = "INSERT INTO Compras (NoFactura, ID_Producto, NIT_Proveedor, Cantidad, " +
                                        "CostoUnitario, CostoTotal, Fecha) VALUES (@NoFactura, @IDProducto, " +
                                        "@NITProveedor, @Cantidad, @CostoUnitario, @CostoTotal, @Fecha)";

                    using (MySqlCommand cmd = new MySqlCommand(queryCompra, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@NoFactura", compra.noFactura);
                        cmd.Parameters.AddWithValue("@IDProducto", compra.idProducto);
                        cmd.Parameters.AddWithValue("@NITProveedor", compra.nitProveedor);
                        cmd.Parameters.AddWithValue("@Cantidad", compra.cantidadComprada);
                        cmd.Parameters.AddWithValue("@CostoUnitario", compra.costoUnitario);
                        cmd.Parameters.AddWithValue("@CostoTotal", compra.costoTotal);
                        cmd.Parameters.AddWithValue("@Fecha", compra.fechaCompra);
                        cmd.ExecuteNonQuery();
                    }

                    // Actualizar inventario (aumentar stock)
                    string queryInventario = "UPDATE Inventario SET Stock = Stock + @Cantidad " +
                                           "WHERE ID_Producto = @IDProducto";

                    using (MySqlCommand cmd = new MySqlCommand(queryInventario, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IDProducto", compra.idProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", compra.cantidadComprada);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al procesar compra: " + ex.Message);
                }
            }
        }

        public void VaciarListaCompras()
        {
            cabezaCompra = null;
            tamanioCompras = 0;
        }
    }
}





