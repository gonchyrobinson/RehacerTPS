using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTareaViewModel{
    public List<ElementoIndexTareaViewModel> tareas;
    public bool permiso{get;set;}
    public IndexTareaViewModel(){
        
    }
    public IndexTareaViewModel(List<Tarea> listTar, List<Usuario> listUs, List<Tablero>listTab,bool permisoAdmin, int idUsLog){
        tareas=new List<ElementoIndexTareaViewModel>();
        foreach (var tar in listTar)
        {
            tareas.Add(new ElementoIndexTareaViewModel(tar,listUs,listTab,idUsLog,permisoAdmin));
        }
        permiso=permisoAdmin;
    }
}
