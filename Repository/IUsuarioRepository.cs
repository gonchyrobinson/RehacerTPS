using System;
using RehacerTPS.Models;
namespace RehacerTPS.Repository;
public interface IUsuarioRepository
{
    public bool CrearUsuario(Usuario usuario);
    public List<Usuario>ListarUsuarios();
    public Usuario? GetUsuarioPorId(int id);
    public bool ModificarUsuario(int id, Usuario modificar);
    public bool EliminarUsuario(int id);
    public string? GetNombreUsuario(int id);
}
