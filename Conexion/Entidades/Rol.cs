using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("rol", Schema = "itp_accesos")]
    public class Rol 
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Codigo")]
        public string Codigo { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("EstaActivo")]
        public bool EstaActivo { get; set; }

        [Column("UltimaActualizacion")]
        public DateTime UltimaActualizacion { get; set; }

        [Column("TenantId")]
        public string? TenantId { get; set; }


        public Rol(string codigo, string nombre, bool estaActivo)
        {
            Codigo = codigo;
            Nombre = nombre;
            EstaActivo = estaActivo;
        }

    }
}
