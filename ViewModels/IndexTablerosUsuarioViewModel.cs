using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTablerosUsuarioViewModel{
    public IndexTablerosUsuarioViewModel()
    {
    }
    public IndexTablerosUsuarioViewModel(List<Tablero> tab,List<Usuario> listaUsuarios,string nombreU,bool permiso,int idUsLog)
    {
        tableros = new List<ElementoIndexTablerosUsuarioViewModel>();
        foreach (var item in tab)
        {
            tableros.Add(new ElementoIndexTablerosUsuarioViewModel(item,listaUsuarios,permiso,idUsLog));
        }
        permisoAsignar=permiso;
        nombreUsuario=nombreU;
    }

    public List<ElementoIndexTablerosUsuarioViewModel> tableros{get;set;}
    public bool permisoAsignar{get;set;}
    public string nombreUsuario{get;set;}
}
