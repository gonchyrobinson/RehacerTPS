using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class AsignarTareaUsuarioViewModel{
    public AsignarTareaUsuarioViewModel()
    {
    }
    public AsignarTareaUsuarioViewModel(int idTarea, List<Usuario> listUsuarios,bool permiso)
    {
        this.idTarea=idTarea;
        usuarios=new List<ElementoAsignarTareaUsuarioViewModel>();
        foreach (var us in listUsuarios)
        {
            usuarios.Add(new ElementoAsignarTareaUsuarioViewModel(us));
        }
        this.permiso=permiso;
    }

    public int idTarea{get;set;}
    [Display(Name = "Usuario Asignado")]
    public int? idUsuarioAsignado{get;set;}
    public List<ElementoAsignarTareaUsuarioViewModel> usuarios{get;set;}
    public bool permiso {get;set;}
    
}
