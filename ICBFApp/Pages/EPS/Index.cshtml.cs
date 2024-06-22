using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.EPS
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString;

        public IndexModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQLServer");
        }

        public List<EPSInfo> listEPS = new List<EPSInfo>();
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                SuccessMessage = TempData["SuccessMessage"] as string;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT * FROM EPS";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    EPSInfo epsInfo  = new EPSInfo();
                                    epsInfo.idEps = reader.GetInt32(0).ToString();
                                    epsInfo.NIT = reader.GetString(1);
                                    epsInfo.nombre = reader.GetString(2);
                                    epsInfo.direccion = reader.GetString(3);
                                    epsInfo.telefono = reader.GetString(4);

                                    listEPS.Add(epsInfo);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay filas en el resultado");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }

        public class EPSInfo
        {
            public string idEps { get; set; }
            public string nombre { get; set; }
            public string NIT { get; set; }
            public string direccion { get; set; }
            public string telefono { get; set; }

        }
    }
}
