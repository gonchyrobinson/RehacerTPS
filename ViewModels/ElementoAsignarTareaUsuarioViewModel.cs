using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoAsignarTareaUsuarioViewModel{
    public ElementoAsignarTareaUsuarioViewModel()
    {
    }
    public ElementoAsignarTareaUsuarioViewModel(Usuario us)
    {
        nombreUs=us.Nombre_de_usuario;
        idUs=us.Id;
    }

    public string nombreUs{get;set;}
    public int idUs{get;set;}
    
}