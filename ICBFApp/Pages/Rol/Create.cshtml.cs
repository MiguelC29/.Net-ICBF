using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.Rol.IndexModel;

namespace ICBFApp.Pages.Rol
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString;

        public CreateModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQLServer");
        }

        public RolInfo rolInfo = new RolInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            rolInfo.nombre = Request.Form["nombre"];

            if (rolInfo.nombre.Length == 0)
            {
                errorMessage = "Debe completar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    String sqlExists = "SELECT COUNT(*) FROM roles WHERE nombre = @nombre";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExists, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@nombre", rolInfo.nombre);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El Rol '" + rolInfo.nombre + "' ya existe. Verifique la información e intente de nuevo.";
                            return Page();
                        }
                    }

                    // Espacio para validar que el rol no exista
                    String sqlInsert = "INSERT INTO Roles (nombre)" +
                        "VALUES (@nombre);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", rolInfo.nombre);

                        command.ExecuteNonQuery();
                    }
                    TempData["SuccessMessage"] = "Rol agregado con exito";
                    return RedirectToPage("/Rol/Index");
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
