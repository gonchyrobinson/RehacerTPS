using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTablerosViewModel{
    public IndexTablerosViewModel()
    {
    }
    public IndexTablerosViewModel(List<Tablero> tab,List<Usuario> listaUsuarios,bool permisoAdmin, int idUsLog)
    {
        tableros = new List<ElementoIndexTablerosViewModel>();
        foreach (var item in tab)
        {
            tableros.Add(new ElementoIndexTablerosViewModel(item,listaUsuarios,permisoAdmin,idUsLog));
        }
    }

    public List<ElementoIndexTablerosViewModel> tableros{get;set;}
    
}
