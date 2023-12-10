using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ElementoModificarTareaViewModelUsuarios
{
    public ElementoModificarTareaViewModelUsuarios()
    {
    }
    public ElementoModificarTareaViewModelUsuarios(Usuario usuario)
    {
        idUs=usuario.Id;
        nombreUs=usuario.Nombre_de_usuario;
    }

    public int idUs{get;set;}
    public string nombreUs{get;set;}
    
}