using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Ninio.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;
using static ICBFApp.Pages.Asistencia.IndexModel;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ICBFApp.Pages.Asistencia
{
    public class CreateModel : PageModel
    {

        public AsistenciaInfo asistenciaInfo = new AsistenciaInfo();
        public List<UsuarioInfo> listaAcudientes { get; set; } = new List<UsuarioInfo>();
        public List<NinioInfo> listaNinios { get; set; } = new List<NinioInfo>();
        public List<DatosBasicosInfo> listaDatosBasicos { get; set; } = new List<DatosBasicosInfo>();
        public string errorMessage = "";
        public string successMessage = "";

        //String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
        //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                Console.WriteLine("No se encontraron datos en la tabla asistencias.");
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
            string fecha = Request.Form["fecha"];
            string estadoNino = Request.Form["estadoNino"];
            string ninioIdString = Request.Form["ninio"];
            int ninioId;

            if (string.IsNullOrEmpty(fecha) || string.IsNullOrEmpty(estadoNino))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sqlInsertAvanceAcademico = "INSERT INTO Asistencias (fecha, estadoNino, idNino)" +
                            "VALUES (@fecha, @estadoNino, @idNino);";

                    using (SqlCommand command2 = new SqlCommand(sqlInsertAvanceAcademico, connection))
                    {
                        command2.Parameters.AddWithValue("@fecha", fecha);
                        command2.Parameters.AddWithValue("@estadoNino", estadoNino);
                        command2.Parameters.AddWithValue("@idNino", ninioId);

                        command2.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "Asistencia creado exitosamente";
                return RedirectToPage("/Asistencia/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
 }

