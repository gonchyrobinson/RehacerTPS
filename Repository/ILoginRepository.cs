using RehacerTPS.Models;

namespace RehacerTPS.Repository;

public interface ILoginRepository{
    public Usuario? Loguear(string nombreUs, string contrasenia);
}
