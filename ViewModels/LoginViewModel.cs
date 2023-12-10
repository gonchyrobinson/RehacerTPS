using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class LoginViewModel{
    public LoginViewModel()
    {
    }
    public LoginViewModel(Usuario us)
    {
        nombreDeUsuario=us.Nombre_de_usuario;
        contrasenia=us.Contrasenia;
    }
    [Required(ErrorMessage = "Este campo es requerido")][Display(Name = "Nombre de Usuario")]
    public string nombreDeUsuario{get;set;}

    [Required(ErrorMessage = "Este campo es requerido")][Display(Name = "Contraseña")]
    public string contrasenia{get;set;}
    
}