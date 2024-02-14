using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoIndexTablerosViewModel{
    public ElementoIndexTablerosViewModel()
    {
    }
    public ElementoIndexTablerosViewModel(Tablero tab,List<Usuario> usuarios, bool permisoAdmin,int idUsLog)
    {
        id = tab.Id;
        id_usuario_asignado = tab.Id_usuario_propietario;
        nombre = tab.Nombre;
        descripcion = tab.Descripcion;
        //Obtengo el nombre del usuario propietario de la tarea para mostrarlo de forma mas clara
        var usuario = usuarios.FirstOrDefault(u => u.Id==id_usuario_asignado,null);
        if(usuario==null)throw(new Exception("No existe el usuario de id "+id_usuario_asignado+" asignado al tablero de id "+id));
        nombreDeUsuario = usuario.Nombre_de_usuario;

        //Esta variable se usa para poder modificar un tablero. Si el usuario es administrador o es propietario del tablero,
        //puede modificarlo o eliminarlo, sino no
        permiso = permisoAdmin || id_usuario_asignado == idUsLog;
    }

    public int id{get;set;}
    public int id_usuario_asignado{get;set;}
    public string nombre{get;set;}
    public string descripcion{get;set;}
    public string nombreDeUsuario{get;set;}
    public bool permiso{get;set;}
    
}
