using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.EPS.IndexModel;
using System.Data.SqlClient;
using static ICBFApp.Pages.Rol.IndexModel;

namespace ICBFApp.Pages.Rol
{
    public class EditModel : PageModel
    {
        private readonly string _connectionString;

        public EditModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQLServer");
        }

        public RolInfo rolInfo = new RolInfo();
        public string errorMessage = "";
        public string successMessage = "";
        
        public void OnGet()
        {
            String idRol = Request.Query["idRol"];

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Roles WHERE idRol = @idRol";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idRol", idRol);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rolInfo.idRol = "" + reader.GetInt32(0);
                                rolInfo.nombre = reader.GetString(1);
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

        public IActionResult OnPost() 
        {
            rolInfo.idRol = Request.Form["idRol"];
            rolInfo.nombre = Request.Form["nombre"];

            if (rolInfo.idRol.Length == 0 || rolInfo.nombre.Length == 0)
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

                    String sqlExists = "SELECT COUNT(*) FROM roles WHERE nombre = @nombre";
                    using (SqlCommand commandCheck = new SqlCommand(sqlExists, connection))
                    {
                        commandCheck.Parameters.AddWithValue("@nombre", rolInfo.nombre);

                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            errorMessage = "El Rol '" + rolInfo.nombre + "' ya existe. Verifique la información e intente de nuevo.";
                            OnGet();
                            return Page();
                        }
                    }

                    // Espacio para validar que el rol no exista
                    String sqlUpdate = "UPDATE Roles SET nombre = @nombre WHERE idRol = @idRol";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@idRol", rolInfo.idRol);
                        command.Parameters.AddWithValue("@nombre", rolInfo.nombre);

                        command.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Rol editado exitosamente";
                return RedirectToPage("/Rol/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
}
