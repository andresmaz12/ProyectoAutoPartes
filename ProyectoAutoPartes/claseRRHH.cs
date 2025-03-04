using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ProyectoAutoPartes
{
    public class claseGestionRRHH
    {
        //Dirección de la base de datos 
        private string enlaceConeccion = "Dirreccion de la base de datos";

        private formMenu form;

        // Constructor con inyección de dependencias
        public claseGestionRRHH(string enlaceConeccion, formMenu form)
        {
            this.enlaceConeccion = enlaceConeccion;
            this.form = form;
        }
    }
}