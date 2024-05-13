using Entidades;
using MensajesExternos;
using Negocio.Clases;
using Negocio.Interfaces;
using System.Text.RegularExpressions;

namespace Negocio.Implementacion
{
    public class ProxyUsuarioRepo : IUsuarioRepo
    {
        private readonly UsuarioRepo _usuarioRepo;
        public ProxyUsuarioRepo(UsuarioRepo usuariorepo)
        {
            _usuarioRepo = usuariorepo;
        }

        public async Task<int> Actualizausuario(ActualizaUsuarioEntrada entrada)
        {
            await Task.CompletedTask;


            if (entrada.EsObjetoValido([entrada.Codigo, entrada.Nombre, entrada.Correo, entrada.Celular]))
                await _usuarioRepo.Actualizausuario(entrada);
            else
                throw new EntradasInvalidasException();

            return 0;
        }

        private static bool SonEntradasSeguras(string[] inputs)
        => !inputs
           .AsEnumerable()
           .Any(x => !EsTextoSeguro(x));

        private static bool EsTextoSeguro(string input)
        {
            var patronJavaScript = @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>";
            return !Regex.IsMatch(input, patronJavaScript, RegexOptions.IgnoreCase);
        }

        public async Task<IEnumerable<Usuario>> DameTodosUsuarios()
        {
            return await _usuarioRepo.DameTodosUsuarios();
        }

        public async Task<Usuario?> DameUsuarioPorId(int id)
        {
            return await _usuarioRepo.DameUsuarioPorId(id);
        }

        public async Task<LoginResponse> Login(string usuario, string contrasenia)
        {
            return await _usuarioRepo.Login(usuario, contrasenia);
        }
    }
}
