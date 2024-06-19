using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;
using static ICBFApp.Pages.Rol.IndexModel;

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
            epsInfo.NIT = Request.Form["NIT"];
            epsInfo.nombre = Request.Form["nombre"];
            epsInfo.direccion = Request.Form["direccion"];
            epsInfo.telefono = Request.Form["telefono"];

            if (epsInfo.nombre.Length == 0 || epsInfo.NIT.Length == 0 || epsInfo.direccion.Length == 0 || epsInfo.telefono.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {
                //(RUTA MIGUEL)String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
                //String connectionString = "RUTA SENA";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Validar si ya existe un nombre
                    String sqlExistsNom = "SELECT COUNT(*) FROM EPS WHERE nombre = @nombre";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExistsNom, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@nombre", epsInfo.nombre);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El nombre '" + epsInfo.nombre + "' ya existe. Verifique la información e intente de nuevo.";
                            return;
                        }
                    }

                    //Validar si ya existe el NIT
                    String sqlExistsNIT = "SELECT COUNT(*) FROM EPS WHERE NIT = @NIT";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExistsNIT, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@NIT", epsInfo.NIT);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El NIT '" + epsInfo.NIT + "' ya existe. Verifique la información e intente de nuevo.";
                            return;
                        }
                    }

                    String sqlInsert = "INSERT INTO EPS (NIT, nombre, direccion, telefono)" +
                        "VALUES (@NIT, @nombre, @direccion, @telefono);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@NIT", epsInfo.NIT);
                        command.Parameters.AddWithValue("@nombre", epsInfo.nombre);
                        command.Parameters.AddWithValue("@direccion", epsInfo.direccion);
                        command.Parameters.AddWithValue("@telefono", epsInfo.telefono);

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
