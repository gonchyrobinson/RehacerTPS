using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class IndexTareaViewModel{
    public List<ElementoIndexTareaViewModel> tareas;
    public IndexTareaViewModel(){
        
    }
    public IndexTareaViewModel(List<Tarea> listTar, List<Usuario> listUs, List<Tablero>listTab){
        tareas=new List<ElementoIndexTareaViewModel>();
        foreach (var tar in listTar)
        {
            tareas.Add(new ElementoIndexTareaViewModel(tar,listUs,listTab));
        }
    }
}
