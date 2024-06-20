using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult OnPost()
        {
            tipoDocInfo.tipo = Request.Form["tipo"];
            if (tipoDocInfo.tipo.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return Page();
            }

            try
            {
                String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
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
                            return Page();
                        }
                    }

                    String sqlInsert = "INSERT INTO TipoDocumento (tipo)" +
                        "VALUES (@tipo);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@tipo", tipoDocInfo.tipo);

                        command.ExecuteNonQuery();
                    }
                    TempData["SuccessMessage"] = "Tipo Documento agregado con éxito";
                    return RedirectToPage("/TipoDocumento/Index");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
}