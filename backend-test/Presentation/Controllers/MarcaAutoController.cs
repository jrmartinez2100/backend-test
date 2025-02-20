using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class MarcaAutoController : ControllerBase
{
    private readonly IMarcaAutoService _marcaAutoService;

    public MarcaAutoController(IMarcaAutoService marcaAutoService)
    {
        _marcaAutoService = marcaAutoService;
    }

    /// <summary>
    /// Obtiene todas las marcas de autos.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var marcas = await _marcaAutoService.GetAll();
            return Ok(marcas);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Obtiene una marca de auto por su ID.
    /// </summary>
    /// <param name="id">ID de la marca de auto</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var marca = await _marcaAutoService.GetById(id);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Busca marcas de autos por nombre.
    /// </summary>
    /// <param name="nombre">Nombre de la marca de auto</param>
    [HttpGet("buscar/{nombre}")]
    public async Task<IActionResult> GetByName(string nombre)
    {
        try
        {
            var marca = await _marcaAutoService.GetMarcaAutosByNombre(nombre);
            if (marca == null)
                return NotFound();
            return Ok(marca);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Crea una nueva marca de auto.
    /// </summary>
    /// <param name="dto">Datos de la marca de auto</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MarcaAutoDto dto)
    {
        try
        {
            // Eliminar el ID del DTO ya que es auto incremental
            dto.Id = 0;
            await _marcaAutoService.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Actualiza una marca de auto existente.
    /// </summary>
    /// <param name="id">ID de la marca de auto</param>
    /// <param name="dto">Datos actualizados de la marca de auto</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MarcaAutoDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest();

            var existingMarca = await _marcaAutoService.GetById(id);
            if (existingMarca == null)
                return NotFound();

            await _marcaAutoService.Update(dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Elimina una marca de auto por su ID.
    /// </summary>
    /// <param name="id">ID de la marca de auto</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingMarca = await _marcaAutoService.GetById(id);
            if (existingMarca == null)
                return NotFound();

            await _marcaAutoService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}

