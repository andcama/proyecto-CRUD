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
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes.Select(c => MapToDto(c));
        }

        public async Task<ClienteDto> GetByIdAsync(string cedula)
        {
            var cliente = await _context.Clientes.FindAsync(cedula);
            return cliente != null ? MapToDto(cliente) : null;
        }

        public async Task<ClienteDto> CreateAsync(ClienteDto clienteDto)
        {
            var cliente = MapToEntity(clienteDto);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return MapToDto(cliente);
        }

        public async Task<ClienteDto> UpdateAsync(string cedula, ClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(cedula);
            if (cliente == null)
                return null;

            // Actualizar propiedades
            cliente.Nombre = clienteDto.Nombre;
            cliente.PrimerApellido = clienteDto.PrimerApellido;
            cliente.SegundoApellido = clienteDto.SegundoApellido;
            cliente.TipoPersona = clienteDto.TipoPersona;
            cliente.FechaNacimiento = clienteDto.FechaNacimiento;

            await _context.SaveChangesAsync();
            return MapToDto(cliente);
        }

        public async Task<bool> DeleteAsync(string cedula)
        {
            // Verificar si el cliente tiene pólizas asociadas
            var tienePolizas = await _context.Polizas
                .AnyAsync(p => p.CedulaAsegurado == cedula);

            if (tienePolizas)
                return false;

            var cliente = await _context.Clientes.FindAsync(cedula);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ClienteDto>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            var clientes = await _context.Clientes
                .Where(c => c.CedulaAsegurado.Contains(searchTerm) ||
                           c.Nombre.Contains(searchTerm) ||
                           c.PrimerApellido.Contains(searchTerm) ||
                           c.SegundoApellido.Contains(searchTerm))
                .ToListAsync();

            return clientes.Select(c => MapToDto(c));
        }

        // Métodos de mapeo
        private ClienteDto MapToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                CedulaAsegurado = cliente.CedulaAsegurado,
                Nombre = cliente.Nombre,
                PrimerApellido = cliente.PrimerApellido,
                SegundoApellido = cliente.SegundoApellido,
                TipoPersona = cliente.TipoPersona,
                FechaNacimiento = cliente.FechaNacimiento
            };
        }

        private Cliente MapToEntity(ClienteDto dto)
        {
            return new Cliente
            {
                CedulaAsegurado = dto.CedulaAsegurado,
                Nombre = dto.Nombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                TipoPersona = dto.TipoPersona,
                FechaNacimiento = dto.FechaNacimiento
            };
        }
    }
}