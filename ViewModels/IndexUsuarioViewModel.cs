using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexUsuarioViewModel
{
    public List<ElementoIndexUsuarioViewModel> usuarios { get; set; }
    public bool permiso{get;set;}
    public int idUsuarioLogueado{get;set;}

    public IndexUsuarioViewModel()
    {
    }
    public IndexUsuarioViewModel(List<Usuario> us, bool permiso, int idUs)
    {
        usuarios = new List<ElementoIndexUsuarioViewModel>();
        foreach (var u in us)
        {
            ElementoIndexUsuarioViewModel el = new ElementoIndexUsuarioViewModel(u);
            usuarios.Add(el);
        }
        this.permiso = permiso;
        idUsuarioLogueado = idUs;
    }
}
