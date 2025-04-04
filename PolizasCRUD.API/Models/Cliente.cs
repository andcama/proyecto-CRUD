using System;
using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string CedulaAsegurado { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        
        [Required]
        [StringLength(50)]
        public string PrimerApellido { get; set; }
        
        [StringLength(50)]
        public string SegundoApellido { get; set; }
        
        [Required]
        [StringLength(20)]
        public string TipoPersona { get; set; }
        
        [Required]
        public DateTime FechaNacimiento { get; set; }
        
        // Relación con pólizas
        public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}