namespace MensajesExternos
{
    public class CreaUsuarioEntrada
    {

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public bool EstaActivo { get; set; }
        public string Contrasenia { get; set; }

        public CreaUsuarioEntrada(string codigo,
                                  string nombre,
                                  string correo,
                                  string celular,
                                  bool estaActivo,
                                  string contrasenia)
        {
            Codigo = codigo;
            Nombre = nombre;
            Correo = correo;
            Celular = celular;
            EstaActivo = estaActivo;
            Contrasenia = contrasenia;
        }
    }


    public class ActualizaUsuarioEntrada
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public bool EstaActivo { get; set; }

        public ActualizaUsuarioEntrada(int id,
                                       string codigo,
                                       string nombre,
                                       string correo,
                                       string celular,
                                       bool estaActivo)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Correo = correo;
            Celular = celular;
            EstaActivo = estaActivo;
        }
    }


    public class UsuarioResumen
    {

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public bool EstaActivo { get; set; }

        public UsuarioResumen(string codigo,
                              string nombre,
                              string correo,
                              string celular,
                              bool estaActivo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Correo = correo;
            Celular = celular;
            EstaActivo = estaActivo;
        }

    }

    public class ListaUsuarioResumen
    {
        public ListaUsuarioResumen(List<UsuarioResumen> usuariosResumen)
        {
            UsuariosResumen = usuariosResumen;
        }

        public List<UsuarioResumen> UsuariosResumen { get; set; }
    }

    public class AsignaRolesAUsuarioEntrada
    {
        public AsignaRolesAUsuarioEntrada(int idUsuario, List<int> idRoles)
        {
            IdUsuario = idUsuario;
            IdsRoles = idRoles;
        }

        public int IdUsuario { get; set; }
        public List<int> IdsRoles { get; set; }
    }


    public class UsuarioRolResumen
    {
        public UsuarioRolResumen(int id, bool estaActivo)
        {
            Id = id;
            EstaActivo = estaActivo;
        }

        public int Id { get; set; }
        public bool EstaActivo { get; set; }
    }

    public class ActualizaUsuarioRolesEntrada
    {
        public ActualizaUsuarioRolesEntrada(List<UsuarioRolResumen> usuariosRol)
        {
            UsuariosRol = usuariosRol;
        }

        public List<UsuarioRolResumen> UsuariosRol { get; set; }

    }
}
