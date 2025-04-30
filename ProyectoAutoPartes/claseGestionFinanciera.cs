using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutoPartes
{
    public class claseGestionFinanciera
    {
        private string connectionString;
        private formMenu form;

        // Constructor con inyección de dependencias
        public claseGestionFinanciera(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
        }

        public int CalculoImpuestos(int porcentajeImpuestos, int ingresosMensuales, int donaciones)
        {
            int resultado = 0;
            resultado = (porcentajeImpuestos/100) * (ingresosMensuales) - donaciones;
            return resultado;
        }
    }
}
