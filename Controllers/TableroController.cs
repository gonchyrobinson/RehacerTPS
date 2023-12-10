using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RehacerTPS.Models;
using RehacerTPS.Repository;
using RehacerTPS.ViewModels;

namespace RehacerTPS.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _manejoTableros;
    private readonly IUsuarioRepository _manejoUsuarios;
    private readonly ITareaRepository _manejotareas;

    public TableroController(ILogger<TableroController> logger, ITableroRepository manejoTableros, IUsuarioRepository manejoUsuarios,ITareaRepository manejotareas)
    {
        _logger = logger;
        this._manejoTableros = manejoTableros;
        this._manejoUsuarios = manejoUsuarios;
        this._manejotareas = manejotareas;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var tableros = _manejoTableros.ListarTableros();
            var usuarios = _manejoUsuarios.ListarUsuarios();
            return View(new IndexTablerosViewModel(tableros, usuarios, PermisoAdmin(),(int)IdUsuarioLogueado()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

   

    [HttpGet]
    public IActionResult Crear()
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            return View(new CrearTableroViewModel((int)IdUsuarioLogueado()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult CrearP(CrearTableroViewModel tab)
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) throw (new Exception("No se cargaron de forma correcta los elementos del formulario"));
            _manejoTableros.CrearTablero(new Tablero(tab));
            return RedirectToAction("IndexTablerosUsuario",new {id=IdUsuarioLogueado()});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult Modificar(int id)
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var modificar = _manejoTableros.GetTablero(id);
            var idUsPropietario = _manejoTableros.idUsuarioPropietario(id);
            var permiso = Permiso((int)idUsPropietario);
            return View(new ModificarTableroViewModel(id, modificar,permiso,(int)idUsPropietario));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

   
//Es correct validar permiso asi?
    [HttpPost]
    public IActionResult ModificarP(ModificarTableroViewModel tabvm)
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) throw (new Exception("Error al cargar los datos en el formulario"));
            if(!Permiso(tabvm.id_usuario_asignado))throw new Exception("El usuario "+NombreUsuarioLogueado()+" de id "+IdUsuarioLogueado()+" intento modificar un tablero de otro usuario");
            var tablero = new Tablero(tabvm);
            _manejoTableros.ModificarTablero(tabvm.id, tablero);
            return RedirectToAction("IndexTablerosUsuario",new{id=tabvm.id_usuario_asignado});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        try
        {
            if (NoSeLogueoElUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var idUs = _manejoTableros.idUsuarioPropietario(id);
            if(!Permiso((int)idUs))throw new Exception("El usuario de id "+IdUsuarioLogueado()+" intento eliminar un tablero de otro usuario");
            int cantTareasEliminadas = _manejotareas.EliminarTareasTablero(id);
            _logger.LogInformation("Se eliminaron "+cantTareasEliminadas+" del tablero de id "+id);
            _manejoTableros.EliminarTablero(id);
            return RedirectToAction("IndexTablerosUsuario",new{id=idUs});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult IndexTablerosUsuario(int id)
    {
        try
        {
            if (NoSeLogueoElUsuario()) return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            // if(HttpContext.Session.GetString("Rol")!="Administrador" && id!=HttpContext.Session.GetInt32("Id"))throw(new Exception($"El usuario {HttpContext.Session.GetString("Nombre")} de id {HttpContext.Session.GetInt32("Id")} intentó ingresar a un tablero del usuario de id "+id));
            var tableros = _manejoTableros.ListarTablerosUsuario(id);
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var nombreUsuario = _manejoUsuarios.GetNombreUsuario(id);
            var permisoAsignar = Permiso(id);
            return (View(new IndexTablerosUsuarioViewModel(tableros, usuarios,nombreUsuario,permisoAsignar,(int)IdUsuarioLogueado())));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private int? IdUsuarioLogueado()
    {
        return HttpContext.Session.GetInt32("Id");
    }
     private bool NoSeLogueoElUsuario()
    {
        return !HttpContext.Session.IsAvailable || NombreUsuarioLogueado() == null;
    }

    private string? NombreUsuarioLogueado()
    {
        return HttpContext.Session.GetString("Nombre");
    }
    private string? RolUsuarioLogueado(){
        return HttpContext.Session.GetString("Rol");
    }
    private bool Permiso(int id){
        return (IdUsuarioLogueado()==id || RolUsuarioLogueado()=="Administrador");
    } 
    private bool PermisoAdmin(){
        return (RolUsuarioLogueado()==Rol.Administrador.ToString());
    }
}
