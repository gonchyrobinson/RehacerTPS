using System.ComponentModel.DataAnnotations;
using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class CambiarEstadoTareaViewModel{
    public CambiarEstadoTareaViewModel()
    {
    }
    public CambiarEstadoTareaViewModel(int id)
    {
        idTarea=id;
    }

    public int idTarea{get;set;}
    [Display(Name = "Nuevo Estado")]
    public Estado nuevoEstado{get;set;}
    
}

