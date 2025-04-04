using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PolizasCRUD.API.DTOs
{
    public class PolizaDto
    {
        [Required(ErrorMessage = "El número de póliza es requerido")]
        [StringLength(50, ErrorMessage = "El número de póliza debe tener máximo 50 caracteres")]
        public string NumeroPoliza { get; set; }
        
        [Required(ErrorMessage = "El tipo de póliza es requerido")]
        public int TipoPolizaId { get; set; }
        public string TipoPolizaNombre { get; set; }
        
        [Required(ErrorMessage = "La cédula del asegurado es requerida")]
        [StringLength(20, ErrorMessage = "La cédula debe tener máximo 20 caracteres")]
        public string CedulaAsegurado { get; set; }
        
        // Información adicional del cliente
        
        [Required(ErrorMessage = "El monto asegurado es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto asegurado debe ser mayor que cero")]
        public decimal MontoAsegurado { get; set; }
        
        [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
        public DateTime FechaVencimiento { get; set; }
        
        [Required(ErrorMessage = "La fecha de emisión es requerida")]
        public DateTime FechaEmision { get; set; }
        
        [Required(ErrorMessage = "El estado de la póliza es requerido")]
        public int EstadoPolizaId { get; set; }
        public string EstadoPolizaNombre { get; set; }
        
        [Required(ErrorMessage = "La prima es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La prima debe ser mayor que cero")]
        public decimal Prima { get; set; }
        
        [Required(ErrorMessage = "El periodo es requerido")]
        public DateTime Periodo { get; set; }
        
        public DateTime FechaInclusion { get; set; }
        
        [Required(ErrorMessage = "La aseguradora es requerida")]
        [StringLength(100, ErrorMessage = "La aseguradora debe tener máximo 100 caracteres")]
        public string Aseguradora { get; set; }
        
        // Lista de IDs de coberturas
        public List<int> CoberturasIds { get; set; } = new List<int>();
        
        // Lista de nombres de coberturas (para mostrar)
        public List<string> CoberturasNombres { get; set; } = new List<string>();
    }
}