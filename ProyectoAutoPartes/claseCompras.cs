using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutoPartes
{
    public class claseCompras
    {

        private readonly string connectionString;
        private readonly IFormDependencies form;

        public claseCompras(string connectionString, IFormDependencies form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }


    }
}
