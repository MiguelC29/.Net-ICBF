using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Numerics;
using static ICBFApp.Pages.Rol.IndexModel;
using static ICBFApp.Pages.Usuario.IndexModel;

namespace ICBFApp.Pages.Usuario
{
    public class CreateModel : PageModel
    {
        public UsuarioInfo usuarioInfo = new UsuarioInfo();
        public DatosBasicos datosBasicos = new DatosBasicos();
        public RolInfo rol = new RolInfo();
        public List<RolInfo> listaRoles = new List<RolInfo>();
        public string errorMessage = "";
        public string successMessage = "";

        String connectionString = "Data Source=PC-MIGUEL-C\\SQLEXPRESS;Initial Catalog=db_ICBF;Integrated Security=True;";
        //String connectionString = "RUTA ANGEL";
        //String connectionString = "RUTA SENA";

        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT * FROM roles";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    RolInfo rolInfo = new RolInfo();
                                    rolInfo.idRol = reader.GetInt32(0).ToString();
                                    rolInfo.nombre = reader.GetString(1);

                                    listaRoles.Add(rolInfo);
                                }
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

        public void OnPost()
        {
            datosBasicos.nombres = Request.Form["nombres"];
            datosBasicos.fechaNacimiento = Request.Form["fechaNacimiento"];
            datosBasicos.celular = Request.Form["celular"];
            datosBasicos.direccion = Request.Form["direccion"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT * FROM roles WHERE idRol = @idRol";
                    String idRol = Request.Form["rol"];
                    
                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        command.Parameters.AddWithValue("@idRol", Convert.ToInt32(idRol));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                reader.Read(); // Mover el cursor al primer registro
                                rol.idRol = reader.GetInt32(0).ToString();
                                rol.nombre = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            usuarioInfo.datosBasicos = datosBasicos;
            usuarioInfo.rol = rol;

            if (datosBasicos.nombres.Length == 0 || datosBasicos.fechaNacimiento.Length == 0 || datosBasicos.celular.Length == 0
               || datosBasicos.direccion.Length == 0 || rol.idRol == null)
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sqlInsertDatosBasicos = "INSERT INTO DatosBasicos (nombres, fechaNacimiento, celular, direccion)" +
                        "VALUES (@nombres, @fechaNacimiento, @celular, @direccion);";
                    // FALTA HACER QUE SI AL INSERTAR DA ERROR, SE DESHAGA EL REGISTRO DE LOS DATOS BASICOS

                    using (SqlCommand command = new SqlCommand(sqlInsertDatosBasicos, connection))
                    {
                        command.Parameters.AddWithValue("@nombres", datosBasicos.nombres);
                        command.Parameters.AddWithValue("@fechaNacimiento", datosBasicos.fechaNacimiento);
                        command.Parameters.AddWithValue("@celular", datosBasicos.celular);
                        command.Parameters.AddWithValue("@direccion", datosBasicos.direccion);

                        command.ExecuteNonQuery();
                    }

                    String sqlSelectDatosBasicos = "SELECT TOP 1 idDatosBasicos FROM DatosBasicos ORDER BY idDatosBasicos DESC";

                    using (SqlCommand command2 = new SqlCommand(sqlSelectDatosBasicos, connection))
                    {
                        using (SqlDataReader reader = command2.ExecuteReader())
                        {
                            // Validar si hay datos
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    datosBasicos.idDatosBasicos = reader.GetInt32(0).ToString();
                                }
                            }
                        }
                    }

                    String sqlInsertUsuario = "INSERT INTO usuarios (idDatosBasicos, idRol)" +
                            "VALUES (@datosBasicos, @rol);";

                    using (SqlCommand command2 = new SqlCommand(sqlInsertUsuario, connection))
                    {
                        command2.Parameters.AddWithValue("@datosBasicos", datosBasicos.idDatosBasicos);
                        command2.Parameters.AddWithValue("@rol", rol.idRol);

                        command2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            datosBasicos.nombres = "";
            datosBasicos.fechaNacimiento = "";
            datosBasicos.celular = "";
            datosBasicos.direccion = "";

            successMessage = "Usuario agregado con éxito";
            Response.Redirect("/Usuario/Index");
        }
    }
}
