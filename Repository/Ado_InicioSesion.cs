using ProyectoFinalCoderHouse2022.Models;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse2022.Repository
{
    public class Ado_InicioSesion
    {
        public static Usuario InicioSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario where nombreUsuario = @nombreUsuario AND Contraseña = @contraseña";

                var paramnombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar);
                paramnombreUsuario.Value = nombreUsuario;
                 var paramcontraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar);
                paramcontraseña.Value = contraseña;

                cmd.Parameters.Add(paramnombreUsuario);
                cmd.Parameters.Add(paramcontraseña);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contraseña = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();
                }
                reader.Close();

                return usuario;
            }
        }







    }
}
