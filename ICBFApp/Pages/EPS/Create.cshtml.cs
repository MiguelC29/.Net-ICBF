using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;

namespace ICBFApp.Pages.EPS
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString;

        public CreateModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQLServer");
        }

        public EPSInfo epsInfo = new EPSInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            epsInfo.NIT = Request.Form["NIT"];
            epsInfo.nombre = Request.Form["nombre"];
            epsInfo.direccion = Request.Form["direccion"];
            epsInfo.telefono = Request.Form["telefono"];

            if (epsInfo.nombre.Length == 0 || epsInfo.NIT.Length == 0 || epsInfo.direccion.Length == 0 || epsInfo.telefono.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                OnGet();
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            OnGet();
                            return Page();
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
                            OnGet();
                            return Page();
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
                    TempData["SuccessMessage"] = "EPS agregada con éxito";
                    return RedirectToPage("/EPS/Index");
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
