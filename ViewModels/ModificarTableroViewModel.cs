using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ModificarTableroViewModel{
    public ModificarTableroViewModel()
    {
    }
    public ModificarTableroViewModel(int id, Tablero t,int idUsProp)
    {
        this.id=id;
        nombre=t.Nombre;
        id_usuario_asignado=idUsProp;
        descripcion=t.Descripcion;
    }

    public int id{get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name = "Nombre del Tablero")]
    public string nombre{get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name = "Id Usuario Asignado")]
    public int id_usuario_asignado{get;set;}
    [Display(Name = "Descripcion")]
    public string? descripcion{get;set;}

}
