using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.Jardin.IndexModel;

namespace ICBFApp.Pages.EPS
{
    public class IndexModel : PageModel
    {

        public List<EPSInfo> listEPS = new List<EPSInfo>();

        public void OnGet()
        {

            try
            {
                //(RUTA MIGUEL) String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "RUTA SENA";

                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                    epsInfo.nombre = reader.GetString(1);

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

        }

    }
}
