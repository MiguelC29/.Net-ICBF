using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static ICBFApp.Pages.Rol.IndexModel;

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
                    String sqlSelect = "SELECT u.idDatosBasicos, nombres, fechaNacimiento, celular, direccion, u.idRol, r.nombre, idUsuario " +
                        "FROM usuarios as u " +
                        "INNER JOIN Roles as r ON u.idRol = r.idRol " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos;";
                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DatosBasicos datosBasicos = new DatosBasicos();
                                    datosBasicos.idDatosBasicos = reader.GetInt32(0).ToString();
                                    datosBasicos.nombres = reader.GetString(1);
                                    datosBasicos.fechaNacimiento = reader.GetDateTime(2).Date.ToShortDateString();
                                    datosBasicos.celular = reader.GetString(3);
                                    datosBasicos.direccion = reader.GetString(4);

                                    RolInfo rol = new RolInfo();
                                    rol.idRol = reader.GetInt32(5).ToString();
                                    rol.nombre = reader.GetString(6);

                                    UsuarioInfo usuarioInfo = new UsuarioInfo();
                                    usuarioInfo.idUsuario = reader.GetInt32(7).ToString();
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
            public string clave { get; set; }
            public DatosBasicos datosBasicos { get; set; }
            public RolInfo rol { get; set; }
        }

        public class DatosBasicos
        {
            public string idDatosBasicos { get; set; }
            public string nombres { get; set; }
            public string fechaNacimiento { get; set; }
            public string celular {  get; set; }
            public string direccion { get; set;}
        }
    }
}
