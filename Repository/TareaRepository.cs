using RehacerTPS.Models;
using System.Data.SQLite;
using System.Drawing;
namespace RehacerTPS.Repository;

public class TareaRepository : ITareaRepository
{
    private readonly string _cadenaDeConexion;

    public TareaRepository(string cadenaDeConexion)
    {
        this._cadenaDeConexion = cadenaDeConexion;
    }

    public bool CrearTarea(Tarea t)
    {
        var queryString = "INSERT INTO Tarea(id_tablero,nombre,estado,descripcion,color,id_usuario_asignado)VALUES(@id_tablero,@nombre,@estado,@descripcion,@color,@id_usuario_asignado)";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero", t.Id_tablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", t.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", t.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", t.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", t.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", t.Id_usuario_asignado));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al crear tarea"));
        return cantFilas > 0;
    }

    public bool Eliminar(int id)
    {
        var queryString = "DELETE FROM Tarea WHERE id=@id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al eliminar la tarea. No existe la tarea de id " + id));
        return cantFilas > 0;
    }

    public Tarea? GetTarea(int id)
    {
        var queryString = "SELECT * FROM Tarea Where id = @id";
        Tarea? tar = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int idTar = Convert.ToInt32(reader["id"]);
                    int idTab = Convert.ToInt32(reader["id_tablero"]);
                    string nombre = reader["nombre"].ToString();
                    Estado estado = (Estado)Convert.ToInt32(reader["estado"]);
                    string? descripcion = null;
                    if (!reader.IsDBNull(4))
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    string? color = null;
                    if (!reader.IsDBNull(5))
                    {
                        color = reader["color"].ToString();
                    }
                    int? id_us_prop = null;
                    if (!reader.IsDBNull(6))
                    {
                        id_us_prop = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    tar = new Tarea(idTar, idTab, nombre, estado, descripcion, color, id_us_prop);
                }
            }
            connection.Close();
        }
        if (tar == null) throw (new Exception($"La tarea de id {id} no existe"));
        return tar;
    }

    public List<Tarea> Listar()
    {
        var queryString = "Select * from Tarea";
        var tareas = new List<Tarea>();
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idTar = Convert.ToInt32(reader["id"]);
                    int idTab = Convert.ToInt32(reader["id_tablero"]);
                    string nombre = reader["nombre"].ToString();
                    Estado estado = (Estado)Convert.ToInt32(reader["estado"]);
                    string? descripcion = null;
                    if (!reader.IsDBNull(4))
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    string? color = null;
                    if (!reader.IsDBNull(5))
                    {
                        color = reader["color"].ToString();
                    }
                    int? id_us_prop = null;
                    if (!reader.IsDBNull(6))
                    {
                        id_us_prop = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    var tar = new Tarea(idTar, idTab, nombre, estado, descripcion, color, id_us_prop);
                    tareas.Add(tar);
                }
            }
            connection.Close();
        }
        return tareas;
    }

    public bool Modificar(int id, Tarea t)
    {
        var queryString = "UPDATE Tarea SET id_tablero=@id_tablero, nombre=@nombre, estado=@estado, descripcion=@descripcion, color=@color, id_usuario_asignado=@id_usuario_asignado WHERE id=@id";
        var cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id_tablero", t.Id_tablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", t.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", t.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", t.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", t.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", t.Id_usuario_asignado));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al Modificar. No existe la tarea de id " + id));
        return cantFilas > 0;
    }
    public List<Tarea> ListarTareasTablero(int idTablero)
    {
        var queryString = "Select * from Tarea WHERE id_tablero = @idTablero";
        var tareas = new List<Tarea>();
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idTar = Convert.ToInt32(reader["id"]);
                    int idTab = Convert.ToInt32(reader["id_tablero"]);
                    string nombre = reader["nombre"].ToString();
                    Estado estado = (Estado)Convert.ToInt32(reader["estado"]);
                    string? descripcion = null;
                    if (!reader.IsDBNull(4))
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    string? color = null;
                    if (!reader.IsDBNull(5))
                    {
                        color = reader["color"].ToString();
                    }
                    int? id_us_prop = null;
                    if (!reader.IsDBNull(6))
                    {
                        id_us_prop = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    var tar = new Tarea(idTar, idTab, nombre, estado, descripcion, color, id_us_prop);
                    tareas.Add(tar);
                }
            }
            connection.Close();
        }
        return tareas;
    }

    public bool AsignarTareaUsuario(int idTarea, int? idUsuario)
    {
        var queryString = "UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id=@idTarea";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al asignar tarea al usuario de id " + idUsuario + " no existe la tarea de id " + idTarea));
        return cantFilas > 0;
    }
    public int? ObtenerIdUsuarioPropietarioDelTableroDeTarea(int idTarea)
    {
        var queryString = "SELECT id_usuario_propietario FROM Tablero INNER JOIN Tarea on Tarea.id_tablero = Tablero.id WHERE Tarea.id=@idTarea";
        int? id = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            var reader = command.ExecuteScalar();
            if (reader != null)
            {
                id = Convert.ToInt32(reader);
            }
            connection.Close();
        }
        if (id == null) throw (new Exception("No existe la tarea de id " + id));
        return id;
    }
    public bool CambiarEstadoTarea(int idTar, Estado nuevoEstado)
    {
        var queryString = "UPDATE Tarea SET estado=@nuevoEstado WHERE id = @id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@nuevoEstado", nuevoEstado));
            command.Parameters.Add(new SQLiteParameter("@id", idTar));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al actualizar la tarea de id " + idTar));
        return cantFilas > 0;
    }
    public int? IdTableroPropietario(int idTarea)
    {
        var queryString = "SELECT id_tablero FROM Tarea WHERE id=@idTarea";
        int? idTab = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            var reader = command.ExecuteScalar();
            connection.Close();
            if (reader == null) throw(new Exception("No existe la tarea de id " + idTarea));
            idTab = Convert.ToInt32(reader);
        }
        return idTab;


    }
    public int? GetIdUsuarioAsignado(int idTarea){
        var queryString = "SELECT id_usuario_asignado FROM Tarea WHERE id=@idTarea";
        int? idUsuarioA = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            var reader = command.ExecuteScalar();
            connection.Close();
            if (reader == null) throw(new Exception("No existe la tarea de id " + idTarea+" o la tarea no tiene un usuario asignado"));
            idUsuarioA = Convert.ToInt32(reader);
        }
        return idUsuarioA;
    }
    public int SetNullTareasUsuario(int idUs){
        var queryString = "UPDATE Tarea SET id_usuario_asignado = NULL WHERE id_usuario_asignado = @idUs";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString,connection);
            command.Parameters.Add(new SQLiteParameter("@idUs",idUs));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        return cantFilas;
    }

    public int EliminarTareasTablero(int idTablero){
        var queryString = "DELETE FROM Tarea WHERE id_tablero = @id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString,connection);
            command.Parameters.Add(new SQLiteParameter("@id",idTablero));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        return cantFilas;
    }
    public int EliminarTareasTablerosDeUsuario(int idUs){
        var queryString = "DELETE FROM Tarea WHERE id_tablero in (SELECT id FROM Tablero WHERE id_usuario_propietario = @id)";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString,connection);
            command.Parameters.Add(new SQLiteParameter("@id",idUs));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        return cantFilas;
    }


}
