using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.Rol.IndexModel;
using static ICBFApp.Pages.TipoDocumento.IndexModel;

namespace ICBFApp.Pages.Usuario
{
    public class IndexModel : PageModel
    {
        public List<UsuarioInfo> listUsuario = new List<UsuarioInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
                //String connectionString = "RUTA ANGEL";
                //String connectionString = "RUTA SENA";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT d.idTipoDocumento, t.tipo, u.idDatosBasicos, identificacion, nombres, fechaNacimiento, celular, direccion, u.idRol, r.nombre, idUsuario " +
                        "FROM usuarios as u " +
                        "INNER JOIN Roles as r ON u.idRol = r.idRol " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos " +
                        "INNER JOIN TipoDocumento as t ON d.idTipoDocumento = t.idTipoDoc;";

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
                                    datosBasicos.celular = reader.GetString(6);
                                    datosBasicos.direccion = reader.GetString(7);

                                    RolInfo rol = new RolInfo();
                                    rol.idRol = reader.GetInt32(8).ToString();
                                    rol.nombre = reader.GetString(9);

                                    UsuarioInfo usuarioInfo = new UsuarioInfo();
                                    usuarioInfo.idUsuario = reader.GetInt32(10).ToString();
                                    usuarioInfo.datosBasicos = datosBasicos;
                                    usuarioInfo.rol = rol;

                                    listUsuario.Add(usuarioInfo);
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

        public class UsuarioInfo
        {
            public string idUsuario { get; set; }
            public string nombreUsuario { get; set; }
            public string correo { get; set; }
            public DatosBasicosInfo datosBasicos { get; set; }
            public RolInfo rol { get; set; }
        }

        public class DatosBasicosInfo
        {
            public string idDatosBasicos { get; set; }
            public string identificacion { get; set; }
            public string nombres { get; set; }
            public string fechaNacimiento { get; set; }
            public string celular { get; set; }
            public string direccion { get; set; }
            public TipoDocInfo tipoDoc { get; set; }
        }
    }
}
