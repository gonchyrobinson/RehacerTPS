using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class CrearTareaViewModel{
    public CrearTareaViewModel()
    {
    }
    public CrearTareaViewModel(List<Usuario> ListUsuarios, List<Tablero> ListTableros)
    {
        usuarios=new List<ElementoCrearTareaViewModelUsuarios>();
        foreach (var us in ListUsuarios)
        {
            usuarios.Add(new ElementoCrearTareaViewModelUsuarios(us));
        }
        tableros = new List<ElementoCrearTaraeViewModelTablero>();
        foreach (var tab in ListTableros)
        {
            tableros.Add(new ElementoCrearTaraeViewModelTablero(tab));
        }
    }
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Tablero Propietario")]
    public int id_tablero{get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Nombre de la Tarea")]

    public string nombre {get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Estado")]

    public Estado estado{get;set;}
    [Display (Name = "Descripción")]
    public string? descripcion{get;set;}
    [Display(Name ="Color")]

    public string? color{get;set;}
    [Display(Name ="Usuario Asignado")]

    public int? id_usuario_asignado{get;set;}
    public List<ElementoCrearTareaViewModelUsuarios> usuarios{get;set;}
    public List<ElementoCrearTaraeViewModelTablero> tableros{get;set;}
    
}
