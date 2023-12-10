using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ModificarTareaViewModel{
    public ModificarTareaViewModel()
    {
    }
    public ModificarTareaViewModel(Tarea t,List<Usuario> ListUsuarios, List<Tablero> ListTableros, bool permisoMod)
    {
        id =t.Id;
        id_tablero=t.Id_tablero;
        nombre=t.Nombre;
        estado=t.Estado;
        descripcion=t.Descripcion;
        color=t.Color;
        id_usuario_asignado=t.Id_usuario_asignado;
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
        permiso=permisoMod;
    }
    public int id{get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Tablero Propietario")]
    public int id_tablero{get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Nombre")]

    public string nombre {get;set;}
    [Required(ErrorMessage ="Este campo es requerido")][Display(Name ="Estado")]

    public Estado estado{get;set;}
    [Display(Name = "Descripción")]
    public string? descripcion{get;set;}
    [Display ( Name ="Color")]
    public string? color{get;set;}
    [Display ( Name ="Usuario Asignado")]

    public int? id_usuario_asignado{get;set;}
    public List<ElementoCrearTareaViewModelUsuarios> usuarios{get;set;}
    public List<ElementoCrearTaraeViewModelTablero> tableros{get;set;}
    public bool permiso{get;set;}
    
}

