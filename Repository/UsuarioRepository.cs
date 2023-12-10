using System.Data.SQLite;
using RehacerTPS.Models;
namespace RehacerTPS.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string cadenaDeConexion;

    public UsuarioRepository(string cadenaDeConexion)
    {
        this.cadenaDeConexion = cadenaDeConexion;
    }

    public bool CrearUsuario(Usuario usuario)
    {
        string queryString = "insert into Usuario(nombre_de_usuario,contrasenia,rol) values(@nombre_de_usuario, @contrasenia, @rol)";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.Nombre_de_usuario));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al crear usuario"));
        return cantFilas > 0;
    }

    public Usuario? GetUsuarioPorId(int id)
    {
        string queryString = "select * from Usuario where id = @id";
        Usuario? us = null;
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int idUs = Convert.ToInt32(reader["id"]);
                    string nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    Rol rol = (Rol)Convert.ToInt32(reader["rol"]);
                    string contrasenia = reader["contrasenia"].ToString();
                    us = new Usuario(idUs, nombre_de_usuario, rol, contrasenia);
                }
            }
            connection.Close();
        }
        if (us == null) throw (new Exception("No existe usuario de id " + id));
        return us;
    }

    public List<Usuario> ListarUsuarios()
    {
        var usuarios = new List<Usuario>();
        string queryString = "SELECT * FROM Usuario";
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idUs = Convert.ToInt32(reader["id"]);
                    string nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    Rol rol = (Rol)Convert.ToInt32(reader["rol"]);
                    string contrasenia = reader["contrasenia"].ToString();
                    Usuario us = new Usuario(idUs, nombre_de_usuario, rol, contrasenia);
                    usuarios.Add(us);
                }
            }
            connection.Close();
        }
        return usuarios;
    }

    public bool ModificarUsuario(int id, Usuario modificar)
    {
        var queryString = "Update Usuario SET nombre_de_usuario = @nombre_de_usuario,rol=@rol,contrasenia=@contrasenia where id = @id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", modificar.Nombre_de_usuario));
            command.Parameters.Add(new SQLiteParameter("@rol", modificar.Rol));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", modificar.Contrasenia));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al modificar usuario de id " + id));

        return cantFilas > 0;
    }
    public bool EliminarUsuario(int id)
    {
        string queryString = "DELETE FROM Usuario WHERE id=@id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al eliminar usuario de id " + id));
        return cantFilas > 0;
    }
    public string? GetNombreUsuario(int id)
    {
        var queryString = "SELECT nombre_de_usuario FROM Usuario WHERE id = @id";
        string? nombre = null;
        using (var connection = new SQLiteConnection(cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            var reader = command.ExecuteScalar();
            connection.Close();
            if (reader == null) throw (new Exception("El usuario de id " + id + " no existe"));
            nombre = reader.ToString();
        }
        return nombre;

    }
}
