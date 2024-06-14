using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Jardin.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Jardin
{
    public class EditModel : PageModel
    {
        public JardinInfo jardinInfo = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";
        String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        //String connectionString = "RUTA ANGEL";
        //String connectionString = "RUTA SENA";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM jardines WHERE idJardin = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                jardinInfo.idJardin = "" + reader.GetInt32(0);
                                jardinInfo.nombre = reader.GetString(1);
                                jardinInfo.direccion = reader.GetString(2);
                                jardinInfo.estado = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            jardinInfo.idJardin = Request.Form["id"];
            jardinInfo.nombre = Request.Form["nombreJardin"];
            jardinInfo.direccion = Request.Form["direccionJardin"];
            jardinInfo.estado = Request.Form["estado"];

            if (jardinInfo.idJardin.Length == 0 || jardinInfo.nombre.Length == 0 || jardinInfo.direccion.Length == 0 || jardinInfo.estado.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sqlExists = "SELECT COUNT(*) FROM jardines WHERE nombre = @nombreJardin";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExists, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@nombreJardin", jardinInfo.nombre);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El Jard�n '" + jardinInfo.nombre + "' ya existe. Verifique la informaci�n e intente de nuevo.";
                            return;
                        }
                    }

                    // Espacio para validar que el jadin no exista
                    String sqlUpdate = "UPDATE jardines SET nombre = @nombreJardin, direccion = @direccionJardin, estado = @estado WHERE idJardin = @id";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@id", jardinInfo.idJardin);
                        command.Parameters.AddWithValue("@nombreJardin", jardinInfo.nombre);
                        command.Parameters.AddWithValue("@direccionJardin", jardinInfo.direccion);
                        command.Parameters.AddWithValue("@estado", jardinInfo.estado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Jardin/Index");
        }
    }
}
