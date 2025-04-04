using System;
using System.ComponentModel.DataAnnotations;

namespace PolizasCRUD.API.DTOs
{
    public class ClienteDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "La cédula debe tener máximo 20 caracteres")]
        public string CedulaAsegurado { get; set; }
        
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre debe tener máximo 50 caracteres")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(50, ErrorMessage = "El primer apellido debe tener máximo 50 caracteres")]
        public string PrimerApellido { get; set; }
        
        [StringLength(50, ErrorMessage = "El segundo apellido debe tener máximo 50 caracteres")]
        public string SegundoApellido { get; set; }
        
        [Required(ErrorMessage = "El tipo de persona es requerido")]
        [StringLength(20, ErrorMessage = "El tipo de persona debe tener máximo 20 caracteres")]
        public string TipoPersona { get; set; }
        
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime FechaNacimiento { get; set; }
        
        // Propiedad para mostrar nombre completo
        public string NombreCompleto => $"{Nombre} {PrimerApellido} {SegundoApellido}".Trim();
    }
}