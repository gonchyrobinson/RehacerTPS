using System.Data.SQLite;
using RehacerTPS.Models;

namespace RehacerTPS.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly string _cadenaDeConexion;

    public LoginRepository(string cadenaDeConexion)
    {
        this._cadenaDeConexion = cadenaDeConexion;
    }

    public Usuario? Loguear(string nombreUs, string contrasenia)
    {
        var queryString = "SELECT * FROM Usuario WHERE nombre_de_usuario =@nombre_de_usuario and contrasenia=@contrasenia";
        Usuario? us=null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString,connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",nombreUs));
            command.Parameters.Add(new SQLiteParameter("@contrasenia",contrasenia));
            connection.Open();
            using (var reader=command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string nombre = reader["nombre_de_usuario"].ToString();
                    string constrasenia = reader["contrasenia"].ToString();
                    Rol rol = (Rol)Convert.ToInt32(reader["rol"]);
                    us=new Usuario(id,nombre,rol,contrasenia);
                }
            }
            connection.Close();
        }
        if(us==null)throw(new Exception($"Intento de acceso inválido - Usuario: {nombreUs} - Clave Ingresada: {contrasenia}"));
        return us;
    }
}