using Conexion;
using Dapper;
using Entidades;
using MensajesExternos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.Clases;
using Negocio.Interfaces;
using System.Data;
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

        public async Task<Usuario?> DameUsuarioPorId(int id)
        {
            var query = $"SELECT * FROM `itp_accesos.usuario` WHERE Id = @id";

            using var conexion = _dapperContext.CreateConnection();

            return await conexion.QuerySingleOrDefaultAsync<Usuario>(query, new { id }) ?? null;
        }

        public async Task<int> Actualizausuario(ActualizaUsuarioEntrada entrada)
        {
            var query = $"UPDATE `itp_accesos.usuario` SET Codigo = @codigo, Nombre = @nombre, Celular = @celular, EstaActivo = @estaActivo WHERE Id = @id;";

            var parameters = new DynamicParameters();

            parameters.Add("id", entrada.Id, DbType.Int32);
            parameters.Add("codigo", entrada.Codigo, DbType.String);
            parameters.Add("nombre", entrada.Nombre, DbType.String);
            parameters.Add("celular", entrada.Celular, DbType.String);
            parameters.Add("estaActivo", entrada.EstaActivo, DbType.Boolean);

            using var conexion = _dapperContext.CreateConnection();

            await conexion.ExecuteAsync(query, parameters);

            return 0;
        }
        public async Task<LoginResponse> Login(string codigo, string contrasenia)
        {

            var queryUsuario = "SELECT * FROM `itp_accesos.usuario` WHERE Codigo = @codigo";

            var parametersUsuario = new DynamicParameters();
            parametersUsuario.Add("codigo", codigo, DbType.String);


            using var conexion = _dapperContext.CreateConnection();

            var usuario = await conexion.QueryFirstOrDefaultAsync<Usuario>(queryUsuario, parametersUsuario);

            if (usuario == null)
                throw new Exception("Usuario o contrasenia incorrecto.");

            var queryUsuarioInfo = "SELECT * FROM `itp_accesos.informacion_usuario` WHERE idUsuario = @idUsuario and contrasenia = @contrasenia";

            var parametersUsuarioInfo = new DynamicParameters();
            parametersUsuarioInfo.Add("idUsuario", usuario.Id, DbType.Int32);
            parametersUsuarioInfo.Add("contrasenia", contrasenia, DbType.String);

            var usuarioInformacion = await conexion.QueryFirstOrDefaultAsync<InformacionUsuario>(queryUsuarioInfo, parametersUsuarioInfo);

            if (usuarioInformacion == null)
                throw new Exception("Usuario o contrasenia incorrecto.");

            var issuer = _configuration["Jwt:Issuer"] ?? "";
            var audience = _configuration["Jwt:Audience"] ?? "";
            var configKey = _configuration["Jwt:Key"] ?? "";

            var key = Encoding.ASCII.GetBytes(configKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =
                    new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Codigo),
                        new Claim(JwtRegisteredClaimNames.Typ, usuario.Codigo == Roles.ADMIN ? Roles.ADMIN : Roles.USER),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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


            return new LoginResponse(stringToken, " ");


        }
    }
}
