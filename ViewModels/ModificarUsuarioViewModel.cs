using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ModificarUsuarioViewModel
{
    public int id{get;set;}
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name = "Nombre de Usuario")]
    public string nombre_de_usuario{get;set;}
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name = "Contraseña")]
    public string contrasenia{get;set;}
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name = "Rol")]
    public Rol rol{get;set;}

    public ModificarUsuarioViewModel()
    {
    }
    public ModificarUsuarioViewModel(Usuario us)
    {
        this.id=us.Id;
        nombre_de_usuario = us.Nombre_de_usuario;
        contrasenia = us.Contrasenia;
        rol = us.Rol;
    }
}
