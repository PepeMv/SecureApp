using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("usuario", Schema = "itp_accesos")]
    public partial class Usuario
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Codigo")]
        public string Codigo { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Correo")]
        public string Correo { get; set; }

        [Column("Celular")]
        public string Celular { get; set; }

        [Column("EstaActivo")]
        public bool EstaActivo { get; set; }

        [Column("UltimaActualizacion")]
        public DateTime UltimaActualizacion { get; set; }

        [Column("TenantId")]
        public string? TenantId { get; set; }


        //public Usuario(string codigo, string nombre, string correo, string celular, bool estaActivo)
        //{
        //    Codigo = codigo;
        //    Nombre = nombre;
        //    Correo = correo;
        //    Celular = celular;
        //    EstaActivo = estaActivo;
        //}

    }
}
