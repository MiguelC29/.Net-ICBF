using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data.SqlClient;
using static ICBFApp.Pages.Jardin.IndexModel;
using static ICBFApp.Pages.Ninio.IndexModel;
using static ICBFApp.Pages.TipoDocumento.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;

namespace ICBFApp.Services
{
    public class GeneratePdfService : IGeneratePdfService
    {
        public List<NinioInfo> listNinio = new List<NinioInfo>();

        public void GetData()
        {
            try
            {
                String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "RUTA ANGEL";
                //String connectionString = "Data Source=BOGAPRCSFFSD108\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT t.tipo, identificacion, nombres, fechaNacimiento, j.nombre, " +
                        "(SELECT nombres FROM Usuarios as u " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos " +
                        "WHERE idUsuario = n.idUsuario) as acudiente, " +
                        "ciudadNacimiento " +
                        "FROM Ninos as n " +
                        "INNER JOIN Jardines as j ON n.idJardin = j.idJardin " +
                        "INNER JOIN DatosBasicos as d ON n.idDatosBasicos = d.idDatosBasicos " +
                        "INNER JOIN TipoDocumento as t ON d.idTipoDocumento = t.idTipoDoc " +
                        "INNER JOIN Usuarios as u ON n.idUsuario = u.idUsuario; ";

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
                                    tipoDocInfo.tipo = reader.GetString(0).ToString();

                                    DatosBasicosInfo datosBasicos = new DatosBasicosInfo();
                                    datosBasicos.tipoDoc = tipoDocInfo;
                                    datosBasicos.identificacion = reader.GetString(1);
                                    datosBasicos.nombres = reader.GetString(2);
                                    datosBasicos.fechaNacimiento = reader.GetDateTime(3).Date.ToShortDateString();

                                    JardinInfo jardin = new JardinInfo();
                                    jardin.nombre = reader.GetString(4);

                                    DatosBasicosInfo datosAcudiente = new DatosBasicosInfo();
                                    datosAcudiente.nombres = reader.GetString(5);

                                    UsuarioInfo acudiente = new UsuarioInfo();
                                    acudiente.datosBasicos = datosAcudiente;

                                    NinioInfo ninio = new NinioInfo();
                                    ninio.ciudadNacimiento = reader.GetString(6);
                                    ninio.edad = calcularEdad(reader.GetDateTime(3).Date.ToShortDateString());
                                    ninio.jardin = jardin;
                                    ninio.acudiente = acudiente;
                                    ninio.datosBasicos = datosBasicos;

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

        public Document GeneratePdfQuest()
        {
            GetData();
            var report = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text("Reporte General")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().Padding(10)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn();
                                columns.RelativeColumn(2);
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background("#212529").Border(0.5f).BorderColor(Colors.Black).Text("Identificación").FontColor("#fff").AlignCenter();
                                header.Cell().Background("#212529").Border(0.5f).BorderColor(Colors.Black).Text("Nombres").FontColor("#fff").AlignCenter();
                                header.Cell().Background("#212529").Border(0.5f).BorderColor(Colors.Black).Text("Edad").FontColor("#fff").AlignCenter();
                                header.Cell().Background("#212529").Border(0.5f).BorderColor(Colors.Black).Text("Acudiente").FontColor("#fff").AlignCenter();
                                header.Cell().Background("#212529").Border(0.5f).BorderColor(Colors.Black).Text("Ciudad Nacimiento").FontColor("#fff").AlignCenter();
                            });

                            foreach (var nino in listNinio)
                            {
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Text(nino.datosBasicos.tipoDoc.tipo + nino.datosBasicos.identificacion).AlignCenter();
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Text(nino.datosBasicos.nombres).AlignCenter();
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Text(nino.edad.ToString()).AlignCenter();
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Text(nino.acudiente.datosBasicos.nombres).AlignCenter();
                                table.Cell().Border(0.5f).BorderColor(Colors.Black).Text(nino.ciudadNacimiento).AlignCenter();
                            }
                        });
                });
            });

            return report;
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
    }
}
