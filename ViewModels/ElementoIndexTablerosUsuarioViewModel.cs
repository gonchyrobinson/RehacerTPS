using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoIndexTablerosUsuarioViewModel{
    public ElementoIndexTablerosUsuarioViewModel()
    {
    }
    public ElementoIndexTablerosUsuarioViewModel(Tablero tab,List<Usuario> usuarios,int idUsLog)
    {
        id = tab.Id;
        id_usuario_asignado = tab.Id_usuario_propietario;
        nombre = tab.Nombre;
        descripcion = tab.Descripcion;
        var usuario = usuarios.FirstOrDefault(u => u.Id==id_usuario_asignado,null);
        if(usuario==null)throw(new Exception("No existe el usuario de id "+id_usuario_asignado+"asignado al tablero de id "+id));
        nombreDeUsuario = usuario.Nombre_de_usuario;
    }

    public int id{get;set;}
    public int id_usuario_asignado{get;set;}
    public string nombre{get;set;}
    public string descripcion{get;set;}
    public string nombreDeUsuario{get;set;}
    
}