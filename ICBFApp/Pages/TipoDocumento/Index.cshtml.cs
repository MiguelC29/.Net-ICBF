using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;

namespace ICBFApp.Pages.TipoDocumento
{
    public class IndexModel : PageModel
    {

        public List<TipoDocInfo> listTipoDocumento = new List<TipoDocInfo>();

        public void OnGet()
        {

             try
            {
                //(RUTA MIGUEL)String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
                //String connectionString = "RUTA SENA";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT * FROM TipoDocumento";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    TipoDocInfo tipoDocInfo = new TipoDocInfo();
                                    tipoDocInfo.idTipoDoc = reader.GetInt32(0).ToString();
                                    tipoDocInfo.tipo = reader.GetString(1);

                                    listTipoDocumento.Add(tipoDocInfo);
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

        public class TipoDocInfo
        {
            public string idTipoDoc { get; set; }
            public string tipo { get; set; }
        }
    }
}
