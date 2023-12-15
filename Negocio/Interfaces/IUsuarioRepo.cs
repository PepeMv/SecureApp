using Entidades;
using MensajesExternos;
using Microsoft.AspNetCore.Mvc;
using Negocio.Clases;

namespace Negocio.Interfaces
{
    public interface IUsuarioRepo
    {
        public Task<IEnumerable<Usuario>> DameTodosUsuarios();
        public Task<Usuario> DameUsuarioPorId(int id);
        Task<int> Actualizausuario(ActualizaUsuarioEntrada entrada);
        public Task<LoginResponse> Login(string usuario, string contrasenia);
    }
}
