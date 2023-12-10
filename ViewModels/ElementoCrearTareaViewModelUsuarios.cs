using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoCrearTareaViewModelUsuarios
{
    public ElementoCrearTareaViewModelUsuarios()
    {
    }
    public ElementoCrearTareaViewModelUsuarios(Usuario usuario)
    {
        idUs=usuario.Id;
        nombreUs=usuario.Nombre_de_usuario;
    }

    public int idUs{get;set;}
    public string nombreUs{get;set;}
    
}
