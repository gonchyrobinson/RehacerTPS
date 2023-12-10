using RehacerTPS.Models;
namespace RehacerTPS.ViewModels;

public class ElementoIndexUsuarioViewModel
{
    public ElementoIndexUsuarioViewModel()
    {
    }
    public ElementoIndexUsuarioViewModel(Usuario us)
    {
        id = us.Id;
        nombre_de_usuario=us.Nombre_de_usuario;
        rol=us.Rol;
    }


    public int id {get;set;}
    public string nombre_de_usuario{get;set;}
    public Rol rol {get;set;}
    
}