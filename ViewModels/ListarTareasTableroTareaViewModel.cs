using RehacerTPS.Models;

namespace RehacerTPS.ViewModels;

public class ListarTareasTableroTareaViewModel{
    public List<ElementoListarTareasTableroViewModel> tareas;
    public string nombreTablero {get;set;}
    public int idTablero{get;set;}
    public bool permisoAdmin{get;set;}
    public ListarTareasTableroTareaViewModel(){
        
    }
    public ListarTareasTableroTareaViewModel(List<Tarea> listTar, List<Usuario> listUs, Tablero tab,string nombreTablero,int idUsuarioLogueado,bool permisoA){
        nombreTablero = tab.Nombre;
        tareas=new List<ElementoListarTareasTableroViewModel>();
        foreach (var tar in listTar)
        {
            tareas.Add(new ElementoListarTareasTableroViewModel(tar,listUs,nombreTablero,idUsuarioLogueado));
        }
        this.nombreTablero=nombreTablero;
        this.permisoAdmin=permisoA;
    }
}
