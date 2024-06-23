using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.AvancesAcademicos.IndexModel;
using static ICBFApp.Pages.EPS.IndexModel;
using static ICBFApp.Pages.Jardin.IndexModel;
using static ICBFApp.Pages.Ninio.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;

namespace ICBFApp.Pages.AvancesAcademicos
{
    public class CreateModel : PageModel
    {

        public AvanceAcademicoInfo avanceAcademicoInfo = new AvanceAcademicoInfo();
        public List<UsuarioInfo> listaAcudientes { get; set; } = new List<UsuarioInfo>();
        public List<NinioInfo> listaNinios { get; set; } = new List<NinioInfo>();
        public List<DatosBasicosInfo> listaDatosBasicos { get; set; } = new List<DatosBasicosInfo>();
        public string errorMessage = "";
        public string successMessage = "";

        private readonly string _connectionString;

        public CreateModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQLServer");
        }

        public void OnGet()
        {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        String sqlNinio = "SELECT Ninos.idNino, DatosBasicos.identificacion " +
                            "FROM Ninos " +
                            "INNER JOIN DatosBasicos ON Ninos.idDatosBasicos = DatosBasicos.idDatosBasicos;";
                        using (SqlCommand command = new SqlCommand(sqlNinio, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Verificar si hay filas en el resultado antes de intentar leer
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                    var idNinio = reader.GetInt32(0).ToString();
                                    var identificacion = reader.GetString(1);
                                    DatosBasicosInfo datosNinios = new DatosBasicosInfo();
                                    datosNinios.identificacion = reader.GetString(1);

                                    listaNinios.Add(new NinioInfo
                                    {
                                        idNinio = reader.GetInt32(0).ToString(),
                                        datosBasicos = datosNinios
                                    });

                                        foreach (var Ninio in listaNinios)
                                        {
                                            Console.WriteLine("List item - id: {0}, identificacion: {1}", Ninio.idNinio, Ninio.datosBasicos.identificacion);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No hay filas en el resultado.");
                                    Console.WriteLine("No se encontraron datos en la tabla jardines.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                    errorMessage = ex.Message;
                }
        }

        public IActionResult OnPost()
        {
            string anioEscolar = Request.Form["anioEscolar"];
            string nivel = Request.Form["nivel"];
            string notas = Request.Form["notas"];
            string descripcion = Request.Form["descripcion"];
            string fechaEntrega = Request.Form["fechaEntrega"];
            string ninioIdString = Request.Form["ninio"];
            int ninioId;

            if (string.IsNullOrEmpty(anioEscolar) || string.IsNullOrEmpty(nivel) || string.IsNullOrEmpty(notas)
                || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(fechaEntrega))
            {
                errorMessage = "Todos los campos son obligatorios";
                return Page();
            }

            if (!int.TryParse(ninioIdString, out ninioId))
            {
                errorMessage = "Niño inválido seleccionado";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    String sqlInsertAvanceAcademico = "INSERT INTO AvancesAcademicos (anioEscolar, nivel, notas, descripcion, fechaEntrega, idNino)" +
                            "VALUES (@anioEscolar, @nivel, @notas, @descripcion, @fechaEntrega, @idNino);";

                    using (SqlCommand command2 = new SqlCommand(sqlInsertAvanceAcademico, connection))
                    {
                        command2.Parameters.AddWithValue("@anioEscolar", anioEscolar);
                        command2.Parameters.AddWithValue("@nivel", nivel);
                        command2.Parameters.AddWithValue("@notas", notas);
                        command2.Parameters.AddWithValue("@descripcion", descripcion);
                        command2.Parameters.AddWithValue("@fechaEntrega", fechaEntrega);
                        command2.Parameters.AddWithValue("@idNino", ninioId);

                        command2.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Avance academico creado exitosamente";
                return RedirectToPage("/AvancesAcademicos/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }

    }
}
