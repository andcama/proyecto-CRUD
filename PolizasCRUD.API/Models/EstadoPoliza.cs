using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.Models
{
    public class EstadoPoliza
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        
        public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}