using System;
using System.Collections.Generic;
using System.Linq;
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
        public int noFactura;
        public string fechaCompra;
        public double pagoIndividual;
        public double pagoTotal;
        public Nodo siguiente;

        public Nodo(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, int nofactura, string fechacompra, double pagoindividual, double pagototal)
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

        public void AgregarDatosFactura(string idproducto, string nombreproducto, string nitcliente, int cantidadllevada, int nofactura, string fechacompra, double pagoindividual, double pagototal)
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

        public void EfecturarCompra()
        {
            Nodo actual = cabeza;
            while (tamanio != 0)
            { 
                 
            }
        }
    }
}
