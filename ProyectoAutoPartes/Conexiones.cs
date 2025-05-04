using System;
using System.Configuration;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // Leer la cadena de conexión desde App.config
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("¡Conexión exitosa!");

                // Ejemplo: Consulta SQL
                string query = "SELECT * FROM usuarios";
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["nombre"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
