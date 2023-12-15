using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("informacion_usuario", Schema = "itp_accesos")]
    public class InformacionUsuario
    {

        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Contrasenia")]
        public string Contrasenia { get; set; }

        [Column("CambioClave")]
        public bool CambioClave { get; set; }


        [Column("UltimoCambioClave")]
        public DateTime UltimoCambioClave { get; set; }

        [Column("UltimaActualizacion")]
        public DateTime UltimaActualizacion { get; set; }

        [Column("TenantId")]
        public string? TenantId { get; set; }

    }
}
