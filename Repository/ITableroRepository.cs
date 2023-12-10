using RehacerTPS.Models;

namespace RehacerTPS.Repository;

public interface ITableroRepository
{
    public bool CrearTablero(Tablero t);
    public Tablero GetTablero(int id);
    public List<Tablero> ListarTableros();
    public List<Tablero> ListarTablerosUsuario(int id);
    public bool ModificarTablero(int id, Tablero t);
    public bool EliminarTablero(int id);
    public int? idUsuarioPropietario(int idTab);

}
