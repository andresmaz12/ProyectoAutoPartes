using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastReport.DataVisualization.Charting;
using MySql.Data.MySqlClient;

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

        public DataTable ObtenerDatosParaGrafico()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Categoria, Valor FROM TablaDatosGrafico";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }

            return dt;
        }
    }

    public class DatoGrafico
    {
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
    }

}
