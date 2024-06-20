using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;
using static ICBFApp.Pages.Jardin.IndexModel;
using static ICBFApp.Pages.TipoDocumento.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;

namespace ICBFApp.Pages.Ninio
{
    public class IndexModel : PageModel
    {

        public List<NinioInfo> listNinio = new List<NinioInfo>();
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                SuccessMessage = TempData["SuccessMessage"] as string;
            }

            try
            {
                String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "RUTA ANGEL";
                //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT d.idTipoDocumento, t.tipo, n.idDatosBasicos, identificacion, nombres, fechaNacimiento, " +
                        "e.idEps, e.nombre, j.idJardin, j.nombre, n.idUsuario, " +
                        "(SELECT idDatosBasicos FROM Usuarios WHERE idUsuario = n.idUsuario) as idDatosBasicosAcudiente, " +
                        "(SELECT nombres FROM Usuarios as u " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos " +
                        "WHERE idUsuario = n.idUsuario) as acudiente, " +
                        "idNino, ciudadNacimiento, tipoSangre " +
                        "FROM Ninos as n " +
                        "INNER JOIN Jardines as j ON n.idJardin = j.idJardin " +
                        "INNER JOIN DatosBasicos as d ON n.idDatosBasicos = d.idDatosBasicos " +
                        "INNER JOIN TipoDocumento as t ON d.idTipoDocumento = t.idTipoDoc " +
                        "INNER JOIN EPS as e ON n.idEps = e.idEps " +
                        "INNER JOIN Usuarios as u ON n.idUsuario = u.idUsuario;";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    TipoDocInfo tipoDocInfo = new TipoDocInfo();
                                    tipoDocInfo.idTipoDoc = reader.GetInt32(0).ToString();
                                    tipoDocInfo.tipo = reader.GetString(1).ToString();

                                    DatosBasicosInfo datosBasicos = new DatosBasicosInfo();
                                    datosBasicos.idDatosBasicos = reader.GetInt32(2).ToString();
                                    datosBasicos.tipoDoc = tipoDocInfo;
                                    datosBasicos.identificacion = reader.GetString(3);
                                    datosBasicos.nombres = reader.GetString(4);
                                    datosBasicos.fechaNacimiento = reader.GetDateTime(5).Date.ToShortDateString();

                                    EPSInfo eps = new EPSInfo();
                                    eps.idEps = reader.GetInt32(6).ToString();
                                    eps.nombre = reader.GetString(7);

                                    JardinInfo jardin = new JardinInfo();
                                    jardin.idJardin = reader.GetInt32(8).ToString();
                                    jardin.nombre = reader.GetString(9);

                                    DatosBasicosInfo datosAcudiente = new DatosBasicosInfo();
                                    datosAcudiente.idDatosBasicos = reader.GetInt32(11).ToString();
                                    datosAcudiente.nombres = reader.GetString(12);

                                    UsuarioInfo acudiente = new UsuarioInfo();
                                    acudiente.idUsuario = reader.GetInt32(10).ToString();
                                    acudiente.datosBasicos = datosAcudiente;

                                    NinioInfo ninio = new NinioInfo();
                                    ninio.idNinio = reader.GetInt32(13).ToString();
                                    ninio.ciudadNacimiento = reader.GetString(14);
                                    ninio.tipoSangre = reader.GetString(15);
                                    ninio.edad = calcularEdad(reader.GetDateTime(5).Date.ToShortDateString());
                                    ninio.jardin = jardin;
                                    ninio.acudiente = acudiente;
                                    ninio.datosBasicos = datosBasicos;
                                    ninio.eps = eps;

                                    listNinio.Add(ninio);
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

        public int calcularEdad(string fechaNacimientoStr)
        {
            DateTime fechaNacimiento;
            bool isValidDate = DateTime.TryParse(fechaNacimientoStr, out fechaNacimiento);

            if (!isValidDate)
            {
                throw new ArgumentException("La fecha de nacimiento no está en un formato válido.");
            }

            DateTime today = DateTime.Today;
            int age = today.Year - fechaNacimiento.Year;

            // Comprueba si el cumpleaños aún no ha ocurrido en el año actual
            if (fechaNacimiento.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public class NinioInfo
        {
            public string idNinio { get; set; }
            public string tipoSangre { get; set; }
            public string ciudadNacimiento { get; set; }
            public string peso { get; set; } //
            public string estatura { get; set; } //
            public int edad { get; set; }
            public JardinInfo jardin { get; set; }
            public UsuarioInfo acudiente { get; set; }
            public DatosBasicosInfo datosBasicos {  get; set; }
            public EPSInfo eps { get; set; }
        }
    }
}
