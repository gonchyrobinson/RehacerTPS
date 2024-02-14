using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RehacerTPS.Models;
using RehacerTPS.Repository;
using RehacerTPS.ViewModels;
namespace RehacerTPS.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _manejoUsuarios;
    private readonly ITableroRepository _manejoTableros;
    private readonly ITareaRepository _manejoTareas;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository manejoUsuarios, ITableroRepository manejoTableros, ITareaRepository manejoTareas)
    {
        _logger = logger;
        this._manejoUsuarios = manejoUsuarios;
        this._manejoTableros = manejoTableros;
        this._manejoTareas = manejoTareas;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var permiso = PermisoAdmin();
            return View(new IndexUsuarioViewModel(usuarios, permiso, (int)IdUsuarioLogueado()));
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
        if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
        return View(new CrearUsuarioViewModel());
    }
    [HttpPost]
    public IActionResult CrearP(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid)
            {
                _logger.LogError("Datos no ingresados no válidos.");
                return RedirectToAction("Crear");
            }
            var creado = new Usuario(usuario);
            _manejoUsuarios.CrearUsuario(creado);
            return RedirectToAction("Index");
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
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!PermisoModEliminar(id)) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " intentó modificar una tarea de otro usuario (de id " + id + ")"));
            var usuario = _manejoUsuarios.GetUsuarioPorId(id);
            return View(new ModificarUsuarioViewModel(usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult ModificarP(ModificarUsuarioViewModel us)
    {
        try
        {
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) throw new Exception("Error al cargar datos del formulario");
            if (!PermisoModEliminar(us.id)) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " intentó modificar una tarea de otro usuario (de id " + us.id + ")"));
            var modificar = new Usuario(us);
            var modificado = _manejoUsuarios.ModificarUsuario(us.id, modificar);
            return RedirectToAction("Index");
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
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!PermisoModEliminar(id)) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " intentó eliminar una tarea de otro usuario (de id " + id + ")"));
            //BORRADO EN CASCADA
            int cantTarEliminadas = _manejoTareas.EliminarTareasTablerosDeUsuario(id);
            _logger.LogInformation("Se eliminaron " + cantTarEliminadas + "tareas del usuario de id " + id);
            int cantTablerosEliminados = _manejoTableros.EliminarTablerosUsuario(id);
            _logger.LogInformation("Se eliminaron " + cantTablerosEliminados + "tableros del Usuario de id " + id);
            var eliminado = _manejoUsuarios.EliminarUsuario(id);
            _logger.LogInformation("Se elimino el usuario de id " + id);
            return RedirectToAction("Index");
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

    //Para que el código esté más claro
    //FUNCIONES PRIVADAS
    private bool NoEstaLogueado()
    {
        return !HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Nombre") == null;
    }
    private int? IdUsuarioLogueado()
    {
        return HttpContext.Session.GetInt32("Id");
    }
    private string? NombreUsuarioLogueado()
    {
        return HttpContext.Session.GetString("Nombre");
    }
    private string? RolUsuarioLogueado()
    {
        return HttpContext.Session.GetString("Rol");
    }
    private bool PermisoAdmin()
    {
        return (RolUsuarioLogueado() == "Administrador");
    }
    private bool PermisoModEliminar(int id)
    {
        return (PermisoAdmin() || IdUsuarioLogueado() == id);
    }
}
