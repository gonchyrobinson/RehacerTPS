using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RehacerTPS.Models;
using RehacerTPS.Repository;
using RehacerTPS.ViewModels;

namespace RehacerTPS.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly ILoginRepository _manejoLogin;

    public LoginController(ILogger<LoginController> logger, ILoginRepository manejoLogin)
    {
        _logger = logger;
        this._manejoLogin = manejoLogin;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }
    [HttpPost]
    public IActionResult Loguear(LoginViewModel login)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception("Error al cargar los datos.");
            var usuario = _manejoLogin.Loguear(login.nombreDeUsuario, login.contrasenia);
            IniciarSession(usuario);
            return RedirectToRoute(new { Controller = "Usuario", Action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToAction("Index");
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
    private void IniciarSession(Usuario us)
    {
        HttpContext.Session.SetString("Nombre", us.Nombre_de_usuario);
        HttpContext.Session.SetString("Rol", us.Rol.ToString());
        HttpContext.Session.SetInt32("Id", us.Id);
        _logger.LogInformation($"El usuario {us.Nombre_de_usuario} ingresó correctamenete");
    }
}

