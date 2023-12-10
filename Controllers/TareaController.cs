using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using RehacerTPS.Models;
using RehacerTPS.Repository;
using RehacerTPS.ViewModels;

namespace RehacerTPS.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _manejoTareas;
    private readonly ITableroRepository _manejoTableros;
    private readonly IUsuarioRepository _manejoUsuarios;

    public TareaController(ILogger<TareaController> logger, ITareaRepository manejoTareas, ITableroRepository manejoTableros, IUsuarioRepository manejoUsuarios)
    {
        _logger = logger;
        this._manejoTareas = manejoTareas;
        this._manejoTableros = manejoTableros;
        this._manejoUsuarios = manejoUsuarios;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var tareas = _manejoTareas.Listar();
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var tableros = _manejoTableros.ListarTableros();
            return (View(new IndexTareaViewModel(tareas, usuarios, tableros,PermisoAdmin(),(int)IdUsuarioLogueado())));
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
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var tableros = _manejoTableros.ListarTablerosUsuario((int)IdUsuarioLogueado());
            return View(new CrearTareaViewModel(usuarios, tableros));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult CrearP(CrearTareaViewModel tarVm)
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) throw (new Exception("Error al cargar los datos del formulario"));
            var tarea = new Tarea(tarVm);
            _manejoTareas.CrearTarea(tarea);
            return RedirectToAction("ListarTareasTablero", new { idTablero = tarea.Id_tablero });
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
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var idUs = _manejoTareas.GetIdUsuarioAsignado(id);
            var permiso = Permiso((int)idUs);
            if (!permiso) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " intentó modificar una tarea de otro usuario"));
            var tareaModificar = _manejoTareas.GetTarea(id);
            var usuarios = _manejoUsuarios.ListarUsuarios();
            List<Tablero> tableros = new List<Tablero>();
            if (PermisoAdmin())
            {
                tableros = _manejoTableros.ListarTableros();
            }
            else
            {
                tableros = _manejoTableros.ListarTablerosUsuario((int)idUs);
            }
            if (RolUsuarioLogueado() == "Administrador")
            {
                return View(new ModificarTareaViewModel(tareaModificar, usuarios, tableros, permiso));
            }
            else
            {
                return RedirectToAction("CambiarEstadoTarea", new { idTar = id });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult CambiarEstadoTarea(int idTar)
    {
        return View(new CambiarEstadoTareaViewModel(idTar));
    }
    [HttpPost]
    public IActionResult CambiarEstadoTareaP(CambiarEstadoTareaViewModel vm)
    {
        try
        {
            if (NoSeLogueoUsuario()) return RedirectToRoute(new { Controller = "Login", Action = "Index" });
            if (!ModelState.IsValid) throw new Exception("Se enviaron los datos del formulario de forma incorrecta");
            var idUsuarioTarea = _manejoTareas.GetIdUsuarioAsignado(vm.idTarea);
            if (!Permiso((int)idUsuarioTarea)) throw (new Exception($"El usuario {NombreUsuarioLogueado()} intentó acceder a una tarea del usuario de id {idUsuarioTarea}"));
            _manejoTareas.CambiarEstadoTarea(vm.idTarea, vm.nuevoEstado);
            var idTab = _manejoTareas.IdTableroPropietario(vm.idTarea);
            return RedirectToAction("ListarTareasTablero", new { idTablero = idTab });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult ModificarP(ModificarTareaViewModel tar)
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            if (!ModelState.IsValid) throw (new Exception("Error al cargar datos del formulario"));
            var permiso = Permiso((int)_manejoTareas.GetIdUsuarioAsignado(tar.id));
            if (!permiso) throw (new Exception("El usuario de id " + IdUsuarioLogueado() + " intentó modificar una tarea de otro usuario"));
            var tarea = new Tarea(tar);
            _manejoTareas.Modificar(tar.id, tarea);
            return RedirectToAction("ListarTareasTablero", new { idTablero = tar.id_tablero });
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
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var idUs = _manejoTareas.ObtenerIdUsuarioPropietarioDelTableroDeTarea(id);
            if (!Permiso((int)idUs)) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " intentó modificar una tarea del usuario de id " + id));
            var id_tablero = _manejoTareas.IdTableroPropietario(id);
            _manejoTareas.Eliminar(id);
            return RedirectToAction("ListarTareasTablero", new { idTablero = id_tablero });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult ListarTareasTablero(int idTablero)
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var tareas = _manejoTareas.ListarTareasTablero(idTablero);
            var tablero = _manejoTableros.GetTablero(idTablero);
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var idUsuarioTablero = _manejoTableros.idUsuarioPropietario(idTablero);
            var permisoAdmin = PermisoAdmin();
            var tableros = _manejoTableros.ListarTableros();
            return View(new ListarTareasTableroTareaViewModel(tareas, usuarios, tableros,tablero, tablero.Nombre, (int)IdUsuarioLogueado(), permisoAdmin));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }



    [HttpGet]
    public IActionResult AsignarTareaUsuario(int idTarea)
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var usuarios = _manejoUsuarios.ListarUsuarios();
            var idPropietarioTablero = _manejoTareas.ObtenerIdUsuarioPropietarioDelTableroDeTarea(idTarea);
            var permiso = Permiso((int)idPropietarioTablero);
            return (View(new AsignarTareaUsuarioViewModel(idTarea, usuarios, permiso)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult AsignarTareaUsuarioP(AsignarTareaUsuarioViewModel vm)
    {
        try
        {
            if (NoSeLogueoUsuario()) return (RedirectToRoute(new { Controller = "Login", Action = "Index" }));
            var id_us_prop  = _manejoTareas.ObtenerIdUsuarioPropietarioDelTableroDeTarea(vm.idTarea);
            if (!Permiso((int)id_us_prop)) throw (new Exception("El usuario " + NombreUsuarioLogueado() + " de id " + IdUsuarioLogueado() + "intento acceder a la tarea de id " + vm.idTarea));
            if (!ModelState.IsValid) throw (new Exception("Se cargaron de forma incorrecta los datos en el formulario"));
            _manejoTareas.AsignarTareaUsuario(vm.idTarea, vm.idUsuarioAsignado);
            var idTab = _manejoTareas.IdTableroPropietario(vm.idTarea);
            return RedirectToAction("ListarTareasTablero",new{idTablero =idTab });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool NoSeLogueoUsuario()
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
    private bool Permiso(int id)
    {
        return (IdUsuarioLogueado() == id || RolUsuarioLogueado() == "Administrador");
    }
    private bool PermisoAdmin()
    {
        return (RolUsuarioLogueado() == "Administrador");
    }
}

