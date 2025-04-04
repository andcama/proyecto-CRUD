using System;

namespace PolizasCRUD.API.DTOs
{
    public class PolizaSearchDto
    {
        public string NumeroPoliza { get; set; }
        public int? TipoPolizaId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string CedulaAsegurado { get; set; }
        public string NombreAsegurado { get; set; }
        
        // Parámetros de paginación
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    
    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}