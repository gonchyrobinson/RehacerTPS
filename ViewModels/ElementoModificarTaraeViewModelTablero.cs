using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoModificarTaraeViewModelTablero
{
    public ElementoModificarTaraeViewModelTablero()
    {
    }
    public ElementoModificarTaraeViewModelTablero(Tablero t)
    {
        idTab=t.Id;
        nombreTab=t.Nombre;
    }

    public int idTab{get;set;}
    public string nombreTab{get;set;}
    
}
