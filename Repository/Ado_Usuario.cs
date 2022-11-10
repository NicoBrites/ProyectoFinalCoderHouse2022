using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse2022.Repository
{
    public class Ado_Usuario
    {
        public static Usuario TraerUsuario(int idUsu)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario where Id = @idUsu";

                var paramIdUsu = new SqlParameter("idUsu", System.Data.SqlDbType.BigInt);
                paramIdUsu.Value = idUsu;

                cmd.Parameters.Add(paramIdUsu);

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
        public static void DeleteUsuario(int idUsuario)
        {
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "DELETE FROM Usuario WHERE id = @idUsu";

                var paramIdUsu = new SqlParameter("idUsu", System.Data.SqlDbType.Int);
                paramIdUsu.Value = idUsuario;

                cmd.Parameters.Add(paramIdUsu);
                cmd.ExecuteNonQuery();
            }
        }
        public static void ModificarUsuario(Usuario usu)
        {
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "UPDATE Usuario SET Nombre = @nombreUsu, Apellido = @apellidoUsu, NombreUsuario = @nombreUsuarioUsu, Contraseña = @contraseñaUsu, Mail = @mailUsu " +
                                   "WHERE ID = @idUsu";


                var paramIdUsu = new SqlParameter("idUsu", System.Data.SqlDbType.BigInt);
                paramIdUsu.Value = usu.Id;

                var paramNombreUsu = new SqlParameter("nombreUsu", System.Data.SqlDbType.VarChar);
                paramNombreUsu.Value = usu.Nombre;

                var parammApellidoUsu = new SqlParameter("apellidoUsu", System.Data.SqlDbType.VarChar);
                parammApellidoUsu.Value = usu.Apellido;

                var paramNombreUsuarioUsu = new SqlParameter("nombreUsuarioUsu", System.Data.SqlDbType.VarChar);
                paramNombreUsuarioUsu.Value = usu.NombreUsuario;

                var paramContraseñaUsu = new SqlParameter("contraseñaUsu", System.Data.SqlDbType.VarChar);
                paramContraseñaUsu.Value = usu.Contraseña;

                var paramMailUsu = new SqlParameter("mailUsu", System.Data.SqlDbType.VarChar);
                paramMailUsu.Value = usu.Mail;

                cmd.Parameters.Add(paramIdUsu);
                cmd.Parameters.Add(paramNombreUsu);
                cmd.Parameters.Add(parammApellidoUsu);
                cmd.Parameters.Add(paramNombreUsuarioUsu);
                cmd.Parameters.Add(paramContraseñaUsu);
                cmd.Parameters.Add(paramMailUsu);
                cmd.ExecuteNonQuery();
            }
        }

        public static void CrearUsuario(Usuario usu)
        {
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                Usuario usuarioViejo = new Usuario();
                usuarioViejo = TraerUsuario(usu.Id);

                if (usu.NombreUsuario != usuarioViejo.NombreUsuario)
                {
                    SqlCommand cmd = conecction.CreateCommand();
                    cmd.CommandText = "Insert into Usuario ( Nombre, Apellido, NombreUsuario, Contraseña, Mail)" +
                                       "values (@nombreUsu, @apellidoUsu, @nombreUsuarioUsu, @contraseñaUsu, @mailUsu)";

                    var paramNombreUsu = new SqlParameter("nombreUsu", System.Data.SqlDbType.VarChar);
                    paramNombreUsu.Value = usu.Nombre;

                    var parammApellidoUsu = new SqlParameter("apellidoUsu", System.Data.SqlDbType.VarChar);
                    parammApellidoUsu.Value = usu.Apellido;

                    var paramNombreUsuarioUsu = new SqlParameter("nombreUsuarioUsu", System.Data.SqlDbType.VarChar);
                    paramNombreUsuarioUsu.Value = usu.NombreUsuario;

                    var paramContraseñaUsu = new SqlParameter("contraseñaUsu", System.Data.SqlDbType.VarChar);
                    paramContraseñaUsu.Value = usu.Contraseña;

                    var paramMailUsu = new SqlParameter("mailUsu", System.Data.SqlDbType.VarChar);
                    paramMailUsu.Value = usu.Mail;

                    cmd.Parameters.Add(paramNombreUsu);
                    cmd.Parameters.Add(parammApellidoUsu);
                    cmd.Parameters.Add(paramNombreUsuarioUsu);
                    cmd.Parameters.Add(paramContraseñaUsu);
                    cmd.Parameters.Add(paramMailUsu);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("El nombre de usuario ya esta en uso..");
                }
            }
        }
       
    }
}

