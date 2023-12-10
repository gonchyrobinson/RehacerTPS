using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoListarTareasTableroViewModel
{

    public int id { get; set; }
    public int id_tablero { get; set; }
    public string nombre { get; set; }
    public Estado estado { get; set; }
    public string? descripcion { get; set; }
    public string? color { get; set; }
    public int? id_usuario_asignado { get; set; }
    public string? nombre_usuario_asignado { get; set; }
    public string nombre_tablero { get; set; }
    public bool permisoModificarEstado{get;set;}
    public bool permisoAsignarUsuario{get;set;}
    public ElementoListarTareasTableroViewModel(Tarea t, List<Usuario> usuarios, List<Tablero> tableros,string nombreTablero, int idUsLogueado)
    {
        id = t.Id;
        id_tablero = t.Id_tablero;
        nombre = t.Nombre;
        estado = t.Estado;
        descripcion = t.Descripcion;
        color = t.Color;
        id_usuario_asignado = t.Id_usuario_asignado;
        var usuario = usuarios.FirstOrDefault(u => u.Id == id_usuario_asignado, null);
        if (usuario != null)
        {
            nombre_usuario_asignado = usuario.Nombre_de_usuario;
        }
        else
        {
            nombre_usuario_asignado = "Ninguno";
        }
        nombre_tablero = nombreTablero;
        var tablero = tableros.FirstOrDefault(tab => tab.Id==id_tablero,null);
        if(tablero == null)throw(new Exception("No existe el tablero de id "+id+" por lo tanto, no se pueden mostrar los tableros (error en el elementoListarTareasViewModel)"));
        var id_usuario_propietario_tablero=tablero.Id_usuario_propietario;
        permisoModificarEstado = id_usuario_asignado==idUsLogueado;
        permisoAsignarUsuario = id_usuario_propietario_tablero == idUsLogueado;
    }

    public ElementoListarTareasTableroViewModel()
    {

    }
}
    