using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.Models
{
    public class Cobertura
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [StringLength(255)]
        public string Descripcion { get; set; }
        
        // Relación muchos a muchos con pólizas
        public virtual ICollection<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
    }
}