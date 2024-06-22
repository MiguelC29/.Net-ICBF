using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Ninio.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Asistencia
{
    public class IndexModel : PageModel
    {

        public List<AsistenciaInfo> listAsistenciaInfo = new List<AsistenciaInfo>();

        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                SuccessMessage = TempData["SuccessMessage"] as string;
            }

            try
            {
                //String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
                //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT DatosBasicos.nombres, DatosBasicos.identificacion, Asistencias.fecha, Asistencias.estadoNino, Asistencias.idAsistencia " +
                        "FROM Asistencias " +
                        "INNER JOIN Ninos ON Asistencias.idNino = Ninos.idNino " +
                        "INNER JOIN DatosBasicos ON Ninos.idDatosBasicos = DatosBasicos.idDatosBasicos;";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Crear un nuevo objeto DatosBasicosInfo
                                    DatosBasicosInfo datosBasicosInfo = new DatosBasicosInfo();
                                    datosBasicosInfo.nombres = reader.GetString(0).ToString();  
                                    datosBasicosInfo.identificacion = reader.GetString(1).ToString(); ;  

                                    // Crear un nuevo objeto AvanceAcademicoInfo
                                    AsistenciaInfo asistenciaInfo = new AsistenciaInfo();
                                    asistenciaInfo.fecha = reader.GetDateTime(2).Date.ToShortDateString();  
                                    asistenciaInfo.estadoNino = reader.GetString(3).ToString(); 
                                    asistenciaInfo.idAsistencia = reader.GetInt32(4).ToString(); 

                                    // Asignar datosBasicosInfo al avanceAcademicoInfo
                                    asistenciaInfo.datosBasicosInfo = datosBasicosInfo;

                                    // Agregar avanceAcademicoInfo a la lista
                                    listAsistenciaInfo.Add(asistenciaInfo);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay filas en el resultado");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }

        public class AsistenciaInfo
        {
            public string idAsistencia { get; set; }
            public string fecha { get; set; }
            public string estadoNino { get; set; }
            public DatosBasicosInfo datosBasicosInfo { get; set; }
            public NinioInfo ninioInfo { get; set; }
            public UsuarioInfo usuarioInfo { get; set; }
        }
    }
}
