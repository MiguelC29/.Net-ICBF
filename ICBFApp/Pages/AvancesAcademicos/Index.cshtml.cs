using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Rol.IndexModel;
using static ICBFApp.Pages.TipoDocumento.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;
using System.Data.SqlClient;
using static ICBFApp.Pages.Ninio.IndexModel;
using System.Data;

namespace ICBFApp.Pages.AvancesAcademicos
{
    public class IndexModel : PageModel
    {

        public List<AvanceAcademicoInfo> listAvanceAcademico = new List<AvanceAcademicoInfo>();

        public void OnGet()
        {
            try
            {
                //String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                String connectionString = "Data Source=DESKTOP-FO2357P\\SQLEXPRESS;Initial Catalog=db_ICBF_final;Integrated Security=True;";
                //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT AvancesAcademicos.idAvanceAcademico, DatosBasicos.nombres, DatosBasicos.identificacion, AvancesAcademicos.nivel, AvancesAcademicos.notas, AvancesAcademicos.descripcion, AvancesAcademicos.fechaEntrega, AvancesAcademicos.anioEscolar " +
                        "FROM AvancesAcademicos " +
                        "INNER JOIN Ninos ON AvancesAcademicos.idNino = Ninos.idNino " +
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
                                    datosBasicosInfo.nombres = reader.GetString(1);  // Nombre
                                    datosBasicosInfo.identificacion = reader.GetString(2);  // Identificación

                                    // Crear un nuevo objeto AvanceAcademicoInfo
                                    AvanceAcademicoInfo avanceAcademicoInfo = new AvanceAcademicoInfo();
                                    avanceAcademicoInfo.nivel = reader.GetString(3);  // Nivel
                                    avanceAcademicoInfo.notas = reader.GetString(4);  // Notas
                                    avanceAcademicoInfo.descripcion = reader.GetString(5);  // Descripción
                                    avanceAcademicoInfo.fechaEntrega = reader.GetDateTime(6).Date.ToShortDateString();
                                    avanceAcademicoInfo.anioEscolar = reader.GetInt32(7) ;// Fecha de Entrega

                                    // Asignar datosBasicosInfo al avanceAcademicoInfo
                                    avanceAcademicoInfo.datosBasicosInfo = datosBasicosInfo;

                                    // Agregar avanceAcademicoInfo a la lista
                                    listAvanceAcademico.Add(avanceAcademicoInfo);
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

        public class AvanceAcademicoInfo
        {
            public int idAvanceAcademico { get; set; }
            public int anioEscolar { get; set; }
            public string nivel {  get; set; }
            public string notas { get; set; }
            public string descripcion { get; set; }
            public string fechaEntrega { get; set; }
            public DatosBasicosInfo datosBasicosInfo { get; set; }
            public NinioInfo ninioInfo { get; set; }
            public UsuarioInfo usuarioInfo { get; set; }
        }

    }
}
