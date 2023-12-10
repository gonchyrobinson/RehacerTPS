using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RehacerTPS.Models;
using RehacerTPS.Repository;
using RehacerTPS.ViewModels;
namespace RehacerTPS.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository manejoUsuarios;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository manejoUsuarios)
    {
        _logger = logger;
        this.manejoUsuarios = manejoUsuarios;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var usuarios = manejoUsuarios.ListarUsuarios();
            var permiso = PermisoAdmin();
            return View(new IndexUsuarioViewModel(usuarios,permiso,(int)IdUsuarioLogueado()));
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
        return View(new CrearUsuarioViewModel());
    }
    [HttpPost]
    public IActionResult CrearP(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (NoEstaLogueado()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) return RedirectToAction("Crear");
            var creado = new Usuario(usuario);
            manejoUsuarios.CrearUsuario(creado);
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
            if(!PermisoModEliminar(id))throw(new Exception("El usuario "+NombreUsuarioLogueado()+" intentó modificar una tarea de otro usuario (de id "+id+")"));
            var usuario = manejoUsuarios.GetUsuarioPorId(id);
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
            if(!ModelState.IsValid)throw new Exception("Error al cargar datos del formulario");
            if(!PermisoModEliminar(us.id))throw(new Exception("El usuario "+NombreUsuarioLogueado()+" intentó modificar una tarea de otro usuario (de id "+us.id+")"));
            var modificar = new Usuario(us);
            var modificado = manejoUsuarios.ModificarUsuario(us.id, modificar);
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
            if(!PermisoModEliminar(id))throw(new Exception("El usuario "+NombreUsuarioLogueado()+" intentó eliminar una tarea de otro usuario (de id "+id+")"));
            var eliminado = manejoUsuarios.EliminarUsuario(id);
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

    private bool NoEstaLogueado()
    {
        return !HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Nombre") == null;
    }
    private int? IdUsuarioLogueado(){
        return HttpContext.Session.GetInt32("Id");
    }
    private string? NombreUsuarioLogueado(){
        return HttpContext.Session.GetString("Nombre");
    }
    private string? RolUsuarioLogueado(){
        return HttpContext.Session.GetString("Rol");
    }
    private bool PermisoAdmin(){
        return (RolUsuarioLogueado()=="Administrador");
    }
    private bool PermisoModEliminar(int id){
        return (PermisoAdmin() || IdUsuarioLogueado()==id);
    }
}
