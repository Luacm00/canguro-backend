using Canguro.API.Data;
using Canguro.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Canguro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SucursalController : ControllerBase
    {
        private readonly SucursalRepository _repo;
        private readonly ILogger<SucursalController> _logger;

        // Constructor que recibe el repositorio de sucursales
        public SucursalController(SucursalRepository repo, ILogger<SucursalController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // Métodos para manejar las sucursales
        // Obtener todas las sucursales con paginación
        [HttpGet]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("GET /api/sucursal llamada con page={Page}, pageSize={PageSize}", page, pageSize);

            var result = await _repo.GetSucursalesAsync(page, pageSize);

            return Ok(new
            {
                data = result.Sucursales,
                total = result.TotalRegistros,
                page,
                pageSize
            });
        }

        // Crear una sucursal
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Models.Sucursal sucursal)
        {
            if (sucursal.FechaCreacion < DateTime.Today)
                return BadRequest("La fecha de creación no puede ser menor a la actual");

            _logger.LogInformation("POST /api/sucursal llamada para crear sucursal con código: {Codigo}", sucursal.Codigo);
            await _repo.InsertSucursalAsync(sucursal);
            return Ok("Sucursal creada exitosamente.");
        }

        // Actualizar una sucursal por ID
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Models.Sucursal sucursal)
        {
            _logger.LogInformation("PUT /api/sucursal llamada para actualizar sucursal ID: {Id}", sucursal.Id);
            await _repo.UpdateSucursalAsync(sucursal);
            return Ok(new { message = "Sucursal actualizada exitosamente" });
        }

        // Eliminar una sucursal por ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogWarning("DELETE /api/sucursal llamada para eliminar lógicamente sucursal ID: {Id}", id);
            await _repo.DeleteSucursalAsync(id);
            return Ok("Sucursal eliminada lógicamente.");
        }
    }
}
