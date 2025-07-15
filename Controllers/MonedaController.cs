using Canguro.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Canguro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonedaController : ControllerBase
    {
        private readonly MonedaRepository _repo;

        public MonedaController(MonedaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var monedas = await _repo.GetAllAsync();
            return Ok(monedas);
        }
    }
}
