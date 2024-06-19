using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.EPS.IndexModel;
using static ICBFApp.Pages.Jardin.IndexModel;
using static ICBFApp.Pages.Ninio.IndexModel;
using static ICBFApp.Pages.Rol.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;

namespace ICBFApp.Pages.Ninio
{
    public class EditModel : PageModel
    {
        public List<JardinInfo> listaJardines { get; set; } = new List<JardinInfo>();
        public List<UsuarioInfo> listaAcudientes { get; set; } = new List<UsuarioInfo>();
        public List<EPSInfo> listaEps { get; set; } = new List<EPSInfo>();
        public string[] listaTiposSangre { get; set; } = new string[] { "O+", "O-", "A+", "A-", "AB+", "AB-" }; //revisar el selected
        public NinioInfo ninio = new NinioInfo();
        public DatosBasicosInfo datosBasicos = new DatosBasicosInfo();
        public EPSInfo epsSelected = new EPSInfo();
        public UsuarioInfo acudienteSelected = new UsuarioInfo();
        public DatosBasicosInfo datosBasicosAcudiente = new DatosBasicosInfo();
        public JardinInfo jardinSelected = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";

        String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        //String connectionString = "RUTA ANGEL";
        //String connectionString = "RUTA SENA";

        public void OnGet()
        {
            String idNino = Request.Query["id"];
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT n.idDatosBasicos, identificacion, nombres, fechaNacimiento, " +
                        "celular, d.direccion, e.idEps, e.nombre, j.idJardin, j.nombre, n.idUsuario, " +
                        "(SELECT idDatosBasicos FROM Usuarios WHERE idUsuario = n.idUsuario) as idDatosBasicosAcudiente, " +
                        "(SELECT identificacion FROM Usuarios as u " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos " +
                        "WHERE idUsuario = n.idUsuario) as acudiente, " +
                        "idNino, ciudadNacimiento, tipoSangre " +
                        "FROM Ninos as n " +
                        "INNER JOIN Jardines as j ON n.idJardin = j.idJardin " +
                        "INNER JOIN DatosBasicos as d ON n.idDatosBasicos = d.idDatosBasicos " +
                        "INNER JOIN TipoDocumento as t ON d.idTipoDocumento = t.idTipoDoc " +
                        "INNER JOIN EPS as e ON n.idEps = e.idEps " +
                        "INNER JOIN Usuarios as u ON n.idUsuario = u.idUsuario " +
                        "WHERE idNino = @idNino;";
                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        command.Parameters.AddWithValue("@idNino", idNino);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                datosBasicos.idDatosBasicos = "" + reader.GetInt32(0);
                                datosBasicos.identificacion = reader.GetString(1);
                                datosBasicos.nombres = reader.GetString(2);
                                datosBasicos.fechaNacimiento = reader.GetDateTime(3).Date.ToString("yyyy-MM-dd");//
                                datosBasicos.celular = reader.GetString(4);
                                datosBasicos.direccion = reader.GetString(5);

                                epsSelected.idEps = reader.GetInt32(6).ToString();
                                epsSelected.nombre = reader.GetString(7);

                                jardinSelected.idJardin = reader.GetInt32(8).ToString();
                                jardinSelected.nombre = reader.GetString(9);

                                acudienteSelected.idUsuario = reader.GetInt32(10).ToString();
                                datosBasicosAcudiente.idDatosBasicos = reader.GetInt32(11).ToString();
                                datosBasicosAcudiente.identificacion = reader.GetString(12);
                                acudienteSelected.datosBasicos = datosBasicosAcudiente;

                                ninio.idNinio = "" + reader.GetInt32(13);
                                ninio.ciudadNacimiento = reader.GetString(14);
                                //ninio.tipoSangre = rolinfoSelected;// FALTA EXCLUIR DEL SELECTED EL VALOR DADO O DE LA LISTA
                                ninio.tipoSangre = reader.GetString(15);
                                ninio.datosBasicos = datosBasicos;
                                ninio.jardin = jardinSelected;
                                ninio.acudiente = acudienteSelected;
                                ninio.eps = epsSelected;
                            } 
                        }
                    }

                    String sqlEps = "SELECT idEps, nombre from eps WHERE idEps != @idEps";
                    using (SqlCommand command = new SqlCommand(sqlEps, connection))
                    {
                        command.Parameters.AddWithValue("@idEps", epsSelected.idEps);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verificar si hay filas en el resultado antes de intentar leer
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var idEps = reader.GetInt32(0).ToString();
                                    var nombre = reader.GetString(1);

                                    listaEps.Add(new EPSInfo
                                    {
                                        idEps = reader.GetInt32(0).ToString(),
                                        nombre = reader.GetString(1)
                                    });

                                    foreach (var eps in listaEps)
                                    {
                                        Console.WriteLine("List item - id: {0}, eps: {1}", eps.idEps, eps.nombre);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay filas en el resultado.");
                                Console.WriteLine("No se encontraron datos en la tabla eps.");
                            }
                        }
                    }

                    String sqlJardines = "SELECT idJardin, nombre FROM jardines WHERE idJardin != @idJardin";
                    using (SqlCommand command = new SqlCommand(sqlJardines, connection))
                    {
                        command.Parameters.AddWithValue("@idJardin", jardinSelected.idJardin);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verificar si hay filas en el resultado antes de intentar leer
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var id = reader.GetInt32(0).ToString();
                                    var nombreJardin = reader.GetString(1);

                                    listaJardines.Add(new JardinInfo
                                    {
                                        idJardin = reader.GetInt32(0).ToString(),
                                        nombre = reader.GetString(1)
                                    });

                                    foreach (var jardin in listaJardines)
                                    {
                                        Console.WriteLine("List item - id: {0}, nombreJardin: {1}", jardin.idJardin, jardin.nombre);
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

                    String sqlAcudiente = "SELECT idUsuario, identificacion FROM Usuarios as u " +
                        "INNER JOIN DatosBasicos as d ON u.idDatosBasicos = d.idDatosBasicos " +
                        "INNER JOIN Roles as r ON u.idRol = r.idRol " +
                        "WHERE r.nombre = 'Acudiente' AND idUsuario != @idUsuario;";
                    using (SqlCommand command = new SqlCommand(sqlAcudiente, connection))
                    {
                        command.Parameters.AddWithValue("@idUsuario", acudienteSelected.idUsuario);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Verificar si hay filas en el resultado antes de intentar leer
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var idUsuario = reader.GetInt32(0).ToString();
                                    var identificacion = reader.GetString(1);
                                    DatosBasicosInfo datosAcudiente = new DatosBasicosInfo();
                                    datosAcudiente.identificacion = reader.GetString(1);

                                    listaAcudientes.Add(new UsuarioInfo
                                    {
                                        idUsuario = reader.GetInt32(0).ToString(),
                                        datosBasicos = datosAcudiente
                                    });

                                    foreach (var acudiente in listaAcudientes)
                                    {
                                        Console.WriteLine("List item - id: {0}, identificacion: {1}", acudiente.idUsuario, acudiente.datosBasicos.identificacion);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay filas en el resultado.");
                                Console.WriteLine("No se encontraron datos en la tabla usuarios.");
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
    }
}
