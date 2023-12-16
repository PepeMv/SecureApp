using Conexion;
using Entidades;
using MensajesExternos;
using Microsoft.Extensions.Configuration;
using Negocio.Clases;
using Negocio.Interfaces;
using System.Text.RegularExpressions;

namespace Negocio.Implementacion
{
    public class ProxyUsuarioRepo : IUsuarioRepo
    {
        private readonly UsuarioRepo _usuarioRepo;
        private readonly DapperContext _dapperContext;
        private readonly IConfiguration _configuration;
        public ProxyUsuarioRepo(UsuarioRepo usuariorepo, DapperContext context, IConfiguration config)
        {
            _usuarioRepo = usuariorepo;
            _dapperContext = context;
            _configuration = config;
        }

        public async Task<int> Actualizausuario(ActualizaUsuarioEntrada entrada)
        {
            await Task.CompletedTask;


            if (SonEntradasSeguras([entrada.Codigo, entrada.Nombre, entrada.Correo, entrada.Celular]))
                await _usuarioRepo.Actualizausuario(entrada);
            else
                throw new Exception("Entradas invalidas para el procesamiento");

            return 0;
        }

        private bool SonEntradasSeguras(string[] inputs)
        => !inputs
           .ToList()
           .Any(x => !EsTextoSeguro(x));

        private bool EsTextoSeguro(string input)
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
