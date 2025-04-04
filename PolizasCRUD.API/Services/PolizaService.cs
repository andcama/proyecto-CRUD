using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PolizasCRUD.API.Data;
using PolizasCRUD.API.DTOs;
using PolizasCRUD.API.Models;

namespace PolizasCRUD.API.Services
{
    public class PolizaService
    {
        private readonly ApplicationDbContext _context;

        public PolizaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolizaDto>> GetAllAsync()
        {
            var polizas = await _context.Polizas
                .Include(p => p.TipoPoliza)
                .Include(p => p.EstadoPoliza)
                .Include(p => p.Cliente)
                .Include(p => p.PolizaCoberturas)
                    .ThenInclude(pc => pc.Cobertura)
                .ToListAsync();

            return polizas.Select(p => MapToDto(p));
        }

        public async Task<PolizaDto> GetByIdAsync(string numeroPoliza)
        {
            var poliza = await _context.Polizas
                .Include(p => p.TipoPoliza)
                .Include(p => p.EstadoPoliza)
                .Include(p => p.Cliente)
                .Include(p => p.PolizaCoberturas)
                    .ThenInclude(pc => pc.Cobertura)
                .FirstOrDefaultAsync(p => p.NumeroPoliza == numeroPoliza);

            return poliza != null ? MapToDto(poliza) : null;
        }

        public async Task<PaginatedResultDto<PolizaDto>> SearchAsync(PolizaSearchDto searchDto)
        {
            var query = _context.Polizas
                .Include(p => p.TipoPoliza)
                .Include(p => p.EstadoPoliza)
                .Include(p => p.Cliente)
                .Include(p => p.PolizaCoberturas)
                    .ThenInclude(pc => pc.Cobertura)
                .AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(searchDto.NumeroPoliza))
                query = query.Where(p => p.NumeroPoliza.Contains(searchDto.NumeroPoliza));

            if (searchDto.TipoPolizaId.HasValue)
                query = query.Where(p => p.TipoPolizaId == searchDto.TipoPolizaId.Value);

            if (searchDto.FechaVencimiento.HasValue)
                query = query.Where(p => p.FechaVencimiento.Date == searchDto.FechaVencimiento.Value.Date);

            if (!string.IsNullOrEmpty(searchDto.CedulaAsegurado))
                query = query.Where(p => p.CedulaAsegurado.Contains(searchDto.CedulaAsegurado));

            if (!string.IsNullOrEmpty(searchDto.NombreAsegurado))
                query = query.Where(p => p.Cliente.Nombre.Contains(searchDto.NombreAsegurado) ||
                                          p.Cliente.PrimerApellido.Contains(searchDto.NombreAsegurado) ||
                                          p.Cliente.SegundoApellido.Contains(searchDto.NombreAsegurado));

            // Contar el total para la paginación
            var totalCount = await query.CountAsync();

            // Aplicar paginación
            var polizas = await query
                .OrderByDescending(p => p.FechaEmision)
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();

            var polizasDto = polizas.Select(p => MapToDto(p)).ToList();

            return new PaginatedResultDto<PolizaDto>
            {
                Items = polizasDto,
                PageNumber = searchDto.PageNumber,
                PageSize = searchDto.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PolizaDto> CreateAsync(PolizaDto polizaDto)
        {
            // Verificar que el cliente exista
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.CedulaAsegurado == polizaDto.CedulaAsegurado);
            if (!clienteExiste)
                throw new KeyNotFoundException($"No existe un cliente con cédula {polizaDto.CedulaAsegurado}");

            // Crear la póliza
            var poliza = new Poliza
            {
                NumeroPoliza = polizaDto.NumeroPoliza,
                TipoPolizaId = polizaDto.TipoPolizaId,
                CedulaAsegurado = polizaDto.CedulaAsegurado,
                MontoAsegurado = polizaDto.MontoAsegurado,
                FechaVencimiento = polizaDto.FechaVencimiento,
                FechaEmision = polizaDto.FechaEmision,
                EstadoPolizaId = polizaDto.EstadoPolizaId,
                Prima = polizaDto.Prima,
                Periodo = polizaDto.Periodo,
                FechaInclusion = DateTime.Now,
                Aseguradora = polizaDto.Aseguradora
            };

            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();

            // Asociar las coberturas
            if (polizaDto.CoberturasIds != null && polizaDto.CoberturasIds.Any())
            {
                foreach (var coberturaId in polizaDto.CoberturasIds)
                {
                    _context.PolizaCoberturas.Add(new PolizaCobertura
                    {
                        NumeroPoliza = poliza.NumeroPoliza,
                        CoberturaId = coberturaId
                    });
                }
                await _context.SaveChangesAsync();
            }

            // Recargar la póliza con todas sus relaciones para el DTO
            return await GetByIdAsync(poliza.NumeroPoliza);
        }

        public async Task<PolizaDto> UpdateAsync(string numeroPoliza, PolizaDto polizaDto)
        {
            var poliza = await _context.Polizas
                .Include(p => p.PolizaCoberturas)
                .FirstOrDefaultAsync(p => p.NumeroPoliza == numeroPoliza);

            if (poliza == null)
                return null;

            // Verificar que el cliente exista si cambia
            if (poliza.CedulaAsegurado != polizaDto.CedulaAsegurado)
            {
                var clienteExiste = await _context.Clientes.AnyAsync(c => c.CedulaAsegurado == polizaDto.CedulaAsegurado);
                if (!clienteExiste)
                    throw new KeyNotFoundException($"No existe un cliente con cédula {polizaDto.CedulaAsegurado}");
            }

            // Actualizar propiedades
            poliza.TipoPolizaId = polizaDto.TipoPolizaId;
            poliza.CedulaAsegurado = polizaDto.CedulaAsegurado;
            poliza.MontoAsegurado = polizaDto.MontoAsegurado;
            poliza.FechaVencimiento = polizaDto.FechaVencimiento;
            poliza.FechaEmision = polizaDto.FechaEmision;
            poliza.EstadoPolizaId = polizaDto.EstadoPolizaId;
            poliza.Prima = polizaDto.Prima;
            poliza.Periodo = polizaDto.Periodo;
            poliza.Aseguradora = polizaDto.Aseguradora;

            // Actualizar coberturas
            if (polizaDto.CoberturasIds != null)
            {
                // Eliminar coberturas existentes
                _context.PolizaCoberturas.RemoveRange(poliza.PolizaCoberturas);

                // Agregar nuevas coberturas
                foreach (var coberturaId in polizaDto.CoberturasIds)
                {
                    _context.PolizaCoberturas.Add(new PolizaCobertura
                    {
                        NumeroPoliza = poliza.NumeroPoliza,
                        CoberturaId = coberturaId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return await GetByIdAsync(poliza.NumeroPoliza);
        }

        public async Task<bool> DeleteAsync(string numeroPoliza)
        {
            var poliza = await _context.Polizas
                .Include(p => p.PolizaCoberturas)
                .FirstOrDefaultAsync(p => p.NumeroPoliza == numeroPoliza);

            if (poliza == null)
                return false;

            // Eliminar coberturas asociadas
            _context.PolizaCoberturas.RemoveRange(poliza.PolizaCoberturas);
            
            // Eliminar la póliza
            _context.Polizas.Remove(poliza);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TipoPoliza>> GetTiposPolizaAsync()
        {
            return await _context.TiposPoliza.ToListAsync();
        }

        public async Task<IEnumerable<EstadoPoliza>> GetEstadosPolizaAsync()
        {
            return await _context.EstadosPoliza.ToListAsync();
        }

        public async Task<IEnumerable<Cobertura>> GetCoberturasAsync()
        {
            return await _context.Coberturas.ToListAsync();
        }

        // Métodos de mapeo
        private PolizaDto MapToDto(Poliza poliza)
        {
            var polizaDto = new PolizaDto
            {
                NumeroPoliza = poliza.NumeroPoliza,
                TipoPolizaId = poliza.TipoPolizaId,
                TipoPolizaNombre = poliza.TipoPoliza?.Nombre,
                CedulaAsegurado = poliza.CedulaAsegurado,
                MontoAsegurado = poliza.MontoAsegurado,
                FechaVencimiento = poliza.FechaVencimiento,
                FechaEmision = poliza.FechaEmision,
                EstadoPolizaId = poliza.EstadoPolizaId,
                EstadoPolizaNombre = poliza.EstadoPoliza?.Nombre,
                Prima = poliza.Prima,
                Periodo = poliza.Periodo,
                FechaInclusion = poliza.FechaInclusion,
                Aseguradora = poliza.Aseguradora
            };

            // Agregar coberturas
            if (poliza.PolizaCoberturas != null)
            {
                polizaDto.CoberturasIds = poliza.PolizaCoberturas.Select(pc => pc.CoberturaId).ToList();
                polizaDto.CoberturasNombres = poliza.PolizaCoberturas
                    .Where(pc => pc.Cobertura != null)
                    .Select(pc => pc.Cobertura.Nombre)
                    .ToList();
            }

            return polizaDto;
        }
    }
}