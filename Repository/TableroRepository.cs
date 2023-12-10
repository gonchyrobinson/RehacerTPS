using System.Data.SQLite;
using RehacerTPS.Models;

namespace RehacerTPS.Repository;

public class TableroRepository : ITableroRepository
{
    private readonly string _cadenaDeConexion;

    public TableroRepository(string cadenaDeConexion)
    {
        this._cadenaDeConexion = cadenaDeConexion;
    }

    public bool CrearTablero(Tablero t)
    {
        var queryString = "INSERT INTO Tablero(id_usuario_propietario,nombre,descripcion)VALUES(@id_usuario_propietario,@nombre,@descripcion)";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", t.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", t.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", t.Descripcion));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al crear tablero"));
        return cantFilas > 0;
    }

    public bool EliminarTablero(int id)
    {
        var queryString = "DELETE FROM Tablero WHERE id=@id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al eliminar el tablero. No existe el tablero de id " + id));
        return cantFilas > 0;
    }

    public Tablero? GetTablero(int id)
    {
        var queryString = "SELECT * FROM Tablero Where id = @id";
        Tablero? tab = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int idTab = Convert.ToInt32(reader["id"]);
                    int id_us_prop = Convert.ToInt32(reader["id_usuario_propietario"]);
                    string nombre = reader["nombre"].ToString();
                    string? descripcion = null;
                    if (!reader.IsDBNull(3))
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    tab = new Tablero(idTab, id_us_prop, nombre, descripcion);
                }
            }
            connection.Close();
        }
        if (tab == null) throw (new Exception($"El tablero de id {id} no existe"));
        return tab;
    }

    public List<Tablero> ListarTableros()
    {
        var queryString = "Select * from Tablero";
        var tableros = new List<Tablero>();
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idTab = Convert.ToInt32(reader["id"]);
                    int id_us_prop = Convert.ToInt32(reader["id_usuario_propietario"]);
                    string nombre = reader["nombre"].ToString();
                    string? descripcion = reader["descripcion"].ToString();
                    Tablero tab = new Tablero(idTab, id_us_prop, nombre, descripcion);
                    tableros.Add(tab);
                }
            }
            connection.Close();
        }
        return tableros;
    }
    public List<Tablero> ListarTablerosUsuario(int idUs)
    {
        var queryString = "Select * from Tablero WHERE id_usuario_propietario=@id";
        var tableros = new List<Tablero>();
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", idUs));
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idTab = Convert.ToInt32(reader["id"]);
                    int id_us_prop = Convert.ToInt32(reader["id_usuario_propietario"]);
                    string nombre = reader["nombre"].ToString();
                    string? descripcion = reader["descripcion"].ToString();
                    Tablero tab = new Tablero(idTab, id_us_prop, nombre, descripcion);
                    tableros.Add(tab);
                }
            }
            connection.Close();
        }
        return tableros;
    }

    public bool ModificarTablero(int id, Tablero t)
    {
        var queryString = "UPDATE Tablero SET id_usuario_propietario=@id_usuario_propietario, nombre=@nombre, descripcion=@descripcion WHERE id = @id";
        int cantFilas = 0;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", t.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", t.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", t.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@id", t.Id));
            connection.Open();
            cantFilas = command.ExecuteNonQuery();
            connection.Close();
        }
        if (cantFilas == 0) throw (new Exception("Error al modificar, no se encontró el tablero de id " + id));
        return cantFilas > 0;
    }
    public int? idUsuarioPropietario(int idTab)
    {
        var queryString = "SELECT id_usuario_propietario FROM Tablero WHERE id=@id";
        int? id = null;
        using (var connection = new SQLiteConnection(_cadenaDeConexion))
        {
            var command = new SQLiteCommand(queryString, connection);
            command.Parameters.Add(new SQLiteParameter("@id", idTab));
            connection.Open();
            var reader = command.ExecuteScalar();
            connection.Close();
            if (reader == null) throw (new Exception("No existe el tablero de id " + idTab));
            id = Convert.ToInt32(reader);
        }
        return id;
    }
}