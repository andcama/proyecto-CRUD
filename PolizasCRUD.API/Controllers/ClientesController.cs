using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizasCRUD.API.DTOs;
using PolizasCRUD.API.Services;

namespace PolizasCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{cedula}")]
        public async Task<ActionResult<ClienteDto>> GetById(string cedula)
        {
            var cliente = await _clienteService.GetByIdAsync(cedula);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create(ClienteDto clienteDto)
        {
            try
            {
                var existente = await _clienteService.GetByIdAsync(clienteDto.CedulaAsegurado);
                if (existente != null)
                    return BadRequest($"Ya existe un cliente con la cédula {clienteDto.CedulaAsegurado}");

                var cliente = await _clienteService.CreateAsync(clienteDto);
                return CreatedAtAction(nameof(GetById), new { cedula = cliente.CedulaAsegurado }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el cliente: {ex.Message}");
            }
        }

        [HttpPut("{cedula}")]
        public async Task<ActionResult<ClienteDto>> Update(string cedula, ClienteDto clienteDto)
        {
            if (cedula != clienteDto.CedulaAsegurado)
                return BadRequest("La cédula del cliente no coincide con el recurso a actualizar");

            try
            {
                var cliente = await _clienteService.UpdateAsync(cedula, clienteDto);
                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }

        [HttpDelete("{cedula}")]
        public async Task<ActionResult> Delete(string cedula)
        {
            try
            {
                var resultado = await _clienteService.DeleteAsync(cedula);
                if (!resultado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el cliente: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Search([FromQuery] string term)
        {
            var clientes = await _clienteService.SearchAsync(term);
            return Ok(clientes);
        }
    }
}