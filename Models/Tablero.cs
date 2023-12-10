using RehacerTPS.ViewModels;

namespace RehacerTPS.Models;

public class Tablero
{
    private int id;
    private int id_usuario_propietario;
    private string nombre;
    private string? descripcion;

    public Tablero()
    {
    }

    public Tablero(int idTab, int id_us_prop, string? nombre, string? descripcion)
    {
        this.id = idTab;
        this.id_usuario_propietario = id_us_prop;
        this.nombre = nombre;
        this.descripcion = descripcion;
    }
    public Tablero(CrearTableroViewModel tabvm)
    {
        id_usuario_propietario = tabvm.id_usuario_asignado;
        nombre = tabvm.nombre;
        descripcion=tabvm.descripcion;
    }
    public Tablero(ModificarTableroViewModel tabvm){
        id=tabvm.id;
        id_usuario_propietario=tabvm.id_usuario_asignado;
        nombre=tabvm.nombre;
        descripcion=tabvm.descripcion;
    }

    public int Id { get => id; set => id = value; }
    public int Id_usuario_propietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
}