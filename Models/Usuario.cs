using System;
using RehacerTPS.ViewModels;
namespace RehacerTPS.Models;
public enum Rol{
    Administrador=1,
    Operativo=2
}
public class Usuario{
     private int id;
     private string nombre_de_usuario;
     private string contrasenia;
     private Rol rol;

    public Usuario()
    {
    }

    public Usuario(CrearUsuarioViewModel usuario)
    {
        this.nombre_de_usuario=usuario.nombre_de_usuario;
        contrasenia=usuario.contrasenia;
        rol=usuario.rol;
    }

    public Usuario(ModificarUsuarioViewModel us)
    {
       id = us.id;
       nombre_de_usuario = us.nombre_de_usuario;
       contrasenia = us.contrasenia;
       rol = us.rol;
    }

    public Usuario(int idUs, string nombre_de_usuario, Rol rol, string contrasenia)
    {
        this.id = idUs;
        this.nombre_de_usuario = nombre_de_usuario;
        this.rol = rol;
        this.contrasenia = contrasenia;
    }

    public int Id { get => id; set => id = value; }
    public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
    public Rol Rol { get => rol; set => rol = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
}
