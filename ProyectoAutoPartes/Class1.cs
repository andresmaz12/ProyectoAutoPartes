using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoAutoPartes
{
    public class Nodo 
    {
        public string idProducto;
        public string nombreProducto;
        public string nitCliente;
        public int cantidadLlevada;
        public string noFactura;
        public string fechaCompra;
        public double pagoIndividual;
        public double pagoTotal;
        public Nodo siguiente;

        public Nodo(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, string fechacompra, double pagoindividual, double pagototal)
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
        private Nodo cabeza;
        private int tamanio = 0;

        public linkedListFacturas()
        {
            cabeza = null;
        }

        public void AgregarDatosFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, string nofactura, string fechacompra, double pagoindividual, double pagototal)
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
            while(actual.nombreProducto != elementoBorrar && actual.siguiente != null)
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

            }
            VaciarLista();
        }

        public void VaciarLista()
        {
            cabeza = null;  // Eliminamos la referencia a la cabeza
            tamanio = 0;    // Restablecemos el tamaño a 0
        }

    }
}
