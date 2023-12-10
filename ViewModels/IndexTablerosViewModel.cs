using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTablerosViewModel{
    public IndexTablerosViewModel()
    {
    }
    public IndexTablerosViewModel(List<Tablero> tab,List<Usuario> listaUsuarios)
    {
        tableros = new List<ElementoIndexTablerosViewModel>();
        foreach (var item in tab)
        {
            tableros.Add(new ElementoIndexTablerosViewModel(item,listaUsuarios));
        }
    }

    public List<ElementoIndexTablerosViewModel> tableros{get;set;}
    
}
