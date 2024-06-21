using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.AvancesAcademicos.IndexModel;
using static ICBFApp.Pages.Asistencia.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Asistencia
{
    public class EditModel : PageModel
    {
        public AsistenciaInfo asistenciaInfo = new AsistenciaInfo();
        public string[] listaEstado { get; set; } = new string[] { "Enfermo", "Sano", "Decaido" };
        public string errorMessage = "";
        public string successMessage = "";

        //String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
        //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

        public void OnGet()
        {
            String idAsistencia = Request.Query["idAsistencia"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Asistencias WHERE idAsistencia = @idAsistencia";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idAsistencia", idAsistencia);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                asistenciaInfo.idAsistencia = reader.GetInt32(0);
                                asistenciaInfo.fecha = reader.GetDateTime(1).Date.ToString("yyyy-MM-dd");
                                asistenciaInfo.estadoNino = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            asistenciaInfo.idAsistencia = Convert.ToInt32(Request.Form["idAvanceAcademico"]);
            asistenciaInfo.fecha = Request.Form["fecha"];
            asistenciaInfo.estadoNino = Request.Form["estadoNino"];

            if (asistenciaInfo.fecha.Length == 0 || asistenciaInfo.estadoNino.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    // Espacio para validar que el jadin no exista
                    String sqlUpdate = "UPDATE Asistencias SET fecha = @fecha, estadoNino = @EstadoNino WHERE idAsistencia = @idAsistencia";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@idAsistencia", asistenciaInfo.idAsistencia);
                        command.Parameters.AddWithValue("@fecha", asistenciaInfo.fecha);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Asistencia/Index");
        }
    }
}
