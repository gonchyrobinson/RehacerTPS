using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class CrearTableroViewModel
{
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name ="Usuario Asignado")]
    public int id_usuario_asignado{get;set;}
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name ="Nombre")]
    public string nombre{get;set;}
    [Display(Name ="Descripción")]
    public string? descripcion{get;set;}
    public CrearTableroViewModel()
    {
    }
    public CrearTableroViewModel(int idUsuario)
    {
        id_usuario_asignado=idUsuario;
    }
}
