using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolizasCRUD.API.Models
{
    public class PolizaCobertura
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string NumeroPoliza { get; set; }
        [ForeignKey("NumeroPoliza")]
        public virtual Poliza Poliza { get; set; }
        
        [Required]
        public int CoberturaId { get; set; }
        [ForeignKey("CoberturaId")]
        public virtual Cobertura Cobertura { get; set; }
    }
}