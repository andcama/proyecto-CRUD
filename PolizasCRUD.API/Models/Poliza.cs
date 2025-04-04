using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolizasCRUD.API.Models
{
    public class Poliza
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string NumeroPoliza { get; set; }
        
        [Required]
        public int TipoPolizaId { get; set; }
        [ForeignKey("TipoPolizaId")]
        public virtual TipoPoliza TipoPoliza { get; set; }
        
        [Required]
        [StringLength(20)]
        public string CedulaAsegurado { get; set; }
        [ForeignKey("CedulaAsegurado")]
        public virtual Cliente Cliente { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoAsegurado { get; set; }
        
        [Required]
        public DateTime FechaVencimiento { get; set; }
        
        [Required]
        public DateTime FechaEmision { get; set; }
        
        [Required]
        public int EstadoPolizaId { get; set; }
        [ForeignKey("EstadoPolizaId")]
        public virtual EstadoPoliza EstadoPoliza { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prima { get; set; }
        
        [Required]
        public DateTime Periodo { get; set; }
        
        public DateTime FechaInclusion { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Aseguradora { get; set; }
        
        // Relación muchos a muchos con coberturas
        public virtual ICollection<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
    }
}