using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoCrearTaraeViewModelTablero
{
    public ElementoCrearTaraeViewModelTablero()
    {
    }
    public ElementoCrearTaraeViewModelTablero(Tablero t)
    {
        idTab=t.Id;
        nombreTab=t.Nombre;
    }

    public int idTab{get;set;}
    public string nombreTab{get;set;}
    
}
