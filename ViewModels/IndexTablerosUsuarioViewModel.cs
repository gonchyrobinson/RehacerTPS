using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTablerosUsuarioViewModel{
    public IndexTablerosUsuarioViewModel()
    {
    }
    public IndexTablerosUsuarioViewModel(List<Tablero> tab,List<Usuario> listaUsuarios,bool permiso,string nombreU)
    {
        tableros = new List<ElementoIndexTablerosViewModel>();
        foreach (var item in tab)
        {
            tableros.Add(new ElementoIndexTablerosViewModel(item,listaUsuarios));
        }
        permisoAsignar=permiso;
        nombreUsuario=nombreU;
    }

    public List<ElementoIndexTablerosViewModel> tableros{get;set;}
    public bool permisoAsignar{get;set;}
    public string nombreUsuario{get;set;}
}
