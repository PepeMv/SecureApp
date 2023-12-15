using Conexion;
using Dapper;
using Entidades;
using MensajesExternos;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.Clases;
using Negocio.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Negocio.Implementacion
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly DapperContext _dapperContext;
        private readonly IConfiguration _configuration;
        public UsuarioRepo(DapperContext context, IConfiguration config)
        {
            _dapperContext = context;
            _configuration = config;
        }

        public async Task<IEnumerable<Usuario>> DameTodosUsuarios()
        {
            var query = "SELECT * FROM `itp_accesos.usuario`";
            using var conexion = _dapperContext.CreateConnection();

            return await conexion.QueryAsync<Usuario>(query);
        }

        public async Task<Usuario> DameUsuarioPorId(int id)
        {
            var query = $"SELECT * FROM `itp_accesos.usuario` WHERE Id = {id}";

            using var conexion = _dapperContext.CreateConnection();

            return await conexion.QuerySingleOrDefaultAsync<Usuario>(query) ?? null;
        }

        public async Task<int> Actualizausuario(ActualizaUsuarioEntrada entrada)
        {
            var query = $"UPDATE `itp_accesos.usuario` SET Codigo='{entrada.Codigo}', Nombre='{entrada.Nombre}', Celular='{entrada.Celular}', EstaActivo='{entrada.EstaActivo}' WHERE Id='{entrada.Id}';";
            
            using var conexion = _dapperContext.CreateConnection();

            var xx = await conexion.ExecuteAsync(query);

            return 0;
        }
        public async Task<LoginResponse> Login(string codigo, string contrasenia)
        {

            var queryUsuario = "SELECT * FROM `itp_accesos.usuario` WHERE Codigo = '" + codigo + "' ";
            using var conexion = _dapperContext.CreateConnection();

            var usuario = await conexion.QueryFirstOrDefaultAsync<Usuario>(queryUsuario);

            if (usuario == null)
                throw new Exception("Usuario o contrasenia incorrecto.");

            var queryUsuarioInfo = "SELECT * FROM `itp_accesos.informacion_usuario` WHERE idUsuario = '" + usuario.Id + "' and contrasenia = '" + contrasenia + "'";

            var usuarioInformacion = await conexion.QueryFirstOrDefaultAsync<InformacionUsuario>(queryUsuarioInfo);

            if (usuarioInformacion == null)
                throw new Exception("Usuario o contrasenia incorrecto.");

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = 
                    new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Codigo),
                        new Claim(JwtRegisteredClaimNames.Jti, 
                        Guid.NewGuid().ToString())
                    }),

                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);


            return new LoginResponse(stringToken, null);

            
        }
    }
}
