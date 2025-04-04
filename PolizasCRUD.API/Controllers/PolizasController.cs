using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizasCRUD.API.DTOs;
using PolizasCRUD.API.Models;
using PolizasCRUD.API.Services;

namespace PolizasCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizasController : ControllerBase
    {
        private readonly PolizaService _polizaService;

        public PolizasController(PolizaService polizaService)
        {
            _polizaService = polizaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolizaDto>>> GetAll()
        {
            var polizas = await _polizaService.GetAllAsync();
            return Ok(polizas);
        }

        [HttpGet("{numeroPoliza}")]
        public async Task<ActionResult<PolizaDto>> GetById(string numeroPoliza)
        {
            var poliza = await _polizaService.GetByIdAsync(numeroPoliza);
            if (poliza == null)
                return NotFound();

            return Ok(poliza);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginatedResultDto<PolizaDto>>> Search([FromBody] PolizaSearchDto searchDto)
        {
            var resultado = await _polizaService.SearchAsync(searchDto);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<PolizaDto>> Create(PolizaDto polizaDto)
        {
            try
            {
                var existente = await _polizaService.GetByIdAsync(polizaDto.NumeroPoliza);
                if (existente != null)
                    return BadRequest($"Ya existe una póliza con el número {polizaDto.NumeroPoliza}");

                var poliza = await _polizaService.CreateAsync(polizaDto);
                return CreatedAtAction(nameof(GetById), new { numeroPoliza = poliza.NumeroPoliza }, poliza);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la póliza: {ex.Message}");
            }
        }

        [HttpPut("{numeroPoliza}")]
        public async Task<ActionResult<PolizaDto>> Update(string numeroPoliza, PolizaDto polizaDto)
        {
            try
            {
                var poliza = await _polizaService.UpdateAsync(numeroPoliza, polizaDto);
                if (poliza == null)
                    return NotFound();

                return Ok(poliza);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la póliza: {ex.Message}");
            }
        }

        [HttpDelete("{numeroPoliza}")]
        public async Task<ActionResult> Delete(string numeroPoliza)
        {
            try
            {
                var resultado = await _polizaService.DeleteAsync(numeroPoliza);
                if (!resultado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la póliza: {ex.Message}");
            }
        }

        [HttpGet("tipos")]
        public async Task<ActionResult<IEnumerable<TipoPoliza>>> GetTiposPoliza()
        {
            var tipos = await _polizaService.GetTiposPolizaAsync();
            return Ok(tipos);
        }

        [HttpGet("estados")]
        public async Task<ActionResult<IEnumerable<EstadoPoliza>>> GetEstadosPoliza()
        {
            var estados = await _polizaService.GetEstadosPolizaAsync();
            return Ok(estados);
        }

        [HttpGet("coberturas")]
        public async Task<ActionResult<IEnumerable<Cobertura>>> GetCoberturas()
        {
            var coberturas = await _polizaService.GetCoberturasAsync();
            return Ok(coberturas);
        }
    }
}