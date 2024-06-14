using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;

namespace ICBFApp.Pages.EPS
{
    public class CreateModel : PageModel
    {

        public EPSInfo epsInfo = new EPSInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            epsInfo.nombre = Request.Form["nombre"];

            if (epsInfo.nombre.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "RUTA SENA";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sqlInsert = "INSERT INTO EPS (nombre)" +
                        "VALUES (@nombre);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", epsInfo.nombre);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            epsInfo.nombre = "";

            successMessage = "EPS agregado con éxito";
            Response.Redirect("/EPS/Index");
        }
    }
}
