using RehacerTPS.ViewModels;

namespace RehacerTPS.Models;
public enum Estado
{
    Ideas = 0,
    ToDo = 1,
    Doing = 2,
    Review = 3,
    Done = 4
}
public class Tarea
{
    private int id;
    private int id_tablero;
    private string nombre;
    private Estado estado;
    private string? descripcion;
    private string? color;
    private int? id_usuario_asignado;

    public Tarea()
    {
    }
    public Tarea(CrearTareaViewModel tarvm)
    {
        id_tablero=tarvm.id_tablero;
        nombre=tarvm.nombre;
        estado=tarvm.estado;
        descripcion=tarvm.descripcion;
        color=tarvm.color;
        id_usuario_asignado=tarvm.id_usuario_asignado;
    }
    public Tarea(ModificarTareaViewModel tarvm)
    {
        id=tarvm.id;
        id_tablero=tarvm.id_tablero;
        nombre=tarvm.nombre;
        estado=tarvm.estado;
        descripcion=tarvm.descripcion;
        color=tarvm.color;
        id_usuario_asignado=tarvm.id_usuario_asignado;
    }

    public Tarea(int id, int id_tablero, string nombre, Estado estado, string? descripcion, string? color, int? id_usuario_asignado)
    {
        this.id = id;
        this.id_tablero = id_tablero;
        this.nombre = nombre;
        this.estado = estado;
        this.descripcion = descripcion;
        this.color = color;
        this.id_usuario_asignado = id_usuario_asignado;
    }

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public Estado Estado { get => estado; set => estado = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public string? Color { get => color; set => color = value; }
    public int? Id_usuario_asignado { get => id_usuario_asignado; set => id_usuario_asignado = value; }
}