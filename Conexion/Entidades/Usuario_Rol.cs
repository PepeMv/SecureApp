using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("usuario_rol", Schema = "itp_accessos")]
    public class Usuario_Rol
    {


        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdRol")]
        public int IdRol { get; set; }

        [Column("EstaActivo")]
        public bool EstaActivo { get; set; }

        [Column("UltimaActualizacion")]
        public DateTime UltimaActualizacion { get; set; }

        [Column("TenantId")]
        public string? TenantId { get; set; }




        public Usuario_Rol(int idUsuario, int idRol, bool estaActivo)
        {
            IdUsuario = idUsuario;
            IdRol = idRol;
            EstaActivo = estaActivo;
        }
    }
}
