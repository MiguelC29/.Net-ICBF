using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.AvancesAcademicos.IndexModel;
using static ICBFApp.Pages.Jardin.IndexModel;

namespace ICBFApp.Pages.AvancesAcademicos
{
    public class EditModel : PageModel
    {
        public AvanceAcademicoInfo avanceAcademicoInfo = new AvanceAcademicoInfo();
        public string[] listaNivel { get; set; } = new string[] { "Natal", "Prenatal", "Parvulo", "Jardin", "Pre-jardin" };
        public string[] listaNota { get; set; } = new string[] { "S", "A", "B"};
        public string errorMessage = "";
        public string successMessage = "";

        //String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
        //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

        public void OnGet()
        {
            String idAvanceAcademico = Request.Query["idAvanceAcademico"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM AvancesAcademicos WHERE idAvanceAcademico = @idAvanceAcademico";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idAvanceAcademico", idAvanceAcademico);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                avanceAcademicoInfo.idAvanceAcademico = reader.GetInt32(0);
                                avanceAcademicoInfo.anioEscolar = reader.GetInt32(1);
                                avanceAcademicoInfo.nivel = reader.GetString(2);
                                avanceAcademicoInfo.notas = reader.GetString(3);
                                avanceAcademicoInfo.descripcion = reader.GetString(4);
                                avanceAcademicoInfo.fechaEntrega = reader.GetDateTime(5).ToString("yyyy-MM-dd");
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
            avanceAcademicoInfo.idAvanceAcademico = Convert.ToInt32(Request.Form["idAvanceAcademico"]);
            avanceAcademicoInfo.anioEscolar = Convert.ToInt32(Request.Form["anioEscolar"]);
            avanceAcademicoInfo.nivel = Request.Form["nivel"];
            avanceAcademicoInfo.notas = Request.Form["notas"];
            avanceAcademicoInfo.descripcion = Request.Form["descripcion"];
            avanceAcademicoInfo.fechaEntrega = Request.Form["fechaEntrega"];

            if (avanceAcademicoInfo.anioEscolar == 0 || avanceAcademicoInfo.idAvanceAcademico == 0 || avanceAcademicoInfo.nivel.Length == 0 || avanceAcademicoInfo.notas.Length == 0 ||
                avanceAcademicoInfo.descripcion.Length == 0 || avanceAcademicoInfo.fechaEntrega.Length == 0) 
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString)) { 

                    // Espacio para validar que el jadin no exista
                    String sqlUpdate = "UPDATE AvancesAcademicos SET anioEscolar = @anioescolar, nivel = @nivel, notas = @notas, descripcion = @descripcion, fechaEntrega = @fechaEntrega WHERE idAvanceAcademico = @idAvanceAcademico WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@idAvanceAcademico", avanceAcademicoInfo.idAvanceAcademico);
                        command.Parameters.AddWithValue("@anioEscolar", avanceAcademicoInfo.anioEscolar);
                        command.Parameters.AddWithValue("@nivel", avanceAcademicoInfo.nivel);
                        command.Parameters.AddWithValue("@notas", avanceAcademicoInfo.notas);
                        command.Parameters.AddWithValue("@descripcion", avanceAcademicoInfo.descripcion);
                        command.Parameters.AddWithValue("@fechaEntrega", avanceAcademicoInfo.fechaEntrega);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/AvancesAcademicos/Index");
        }
    }
}