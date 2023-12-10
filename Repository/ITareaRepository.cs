using RehacerTPS.Models;

namespace RehacerTPS.Repository;

public interface ITareaRepository{
    public bool CrearTarea(Tarea t);
    public Tarea? GetTarea(int id);
    public bool Modificar(int id, Tarea t);
    public bool Eliminar(int id);
    public List<Tarea> Listar();
    public List<Tarea> ListarTareasTablero(int idTablero);
    public bool AsignarTareaUsuario(int idTarea, int? idUsuario);
    public int? ObtenerIdUsuarioPropietarioDelTableroDeTarea(int idTarea);
    public bool CambiarEstadoTarea(int idTar, Estado nuevoEstado);
    public int? IdTableroPropietario(int idTarea);
    public int? GetIdUsuarioAsignado(int id);

}
