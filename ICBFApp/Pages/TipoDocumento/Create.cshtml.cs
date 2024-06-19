using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.EPS.IndexModel;
using System.Data.SqlClient;
using static ICBFApp.Pages.TipoDocumento.IndexModel;

namespace ICBFApp.Pages.TipoDocumento
{
    public class CreateModel : PageModel
    {

        public TipoDocInfo tipoDocInfo = new TipoDocInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            tipoDocInfo.tipo = Request.Form["tipo"];
            if (tipoDocInfo.tipo.Length == 0)
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

                    //Validar si ya existe el tipo
                    String sqlExistsNIT = "SELECT COUNT(*) FROM TipoDocumento WHERE tipo = @tipo";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExistsNIT, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@tipo", tipoDocInfo.tipo);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El tipo de documento '" + tipoDocInfo.tipo + "' ya existe. Verifique la información e intente de nuevo.";
                            return;
                        }
                    }

                    String sqlInsert = "INSERT INTO TipoDocumento (tipo)" +
                        "VALUES (@tipo);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@tipo", tipoDocInfo.tipo);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            tipoDocInfo.tipo = "";

            successMessage = "Tipo Documento agregado con éxito";
            Response.Redirect("/TipoDocumento/Index");
        }
    }
}
