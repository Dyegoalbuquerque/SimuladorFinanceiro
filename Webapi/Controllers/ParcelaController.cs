using Microsoft.AspNetCore.Mvc;
using Webapi.Domain.Services.Abstract;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ParcelaController : ControllerBase
    {
        private IParcelaService ParcelaService {get; set; }
        public ParcelaController(IParcelaService parcelaService) 
        {
            this.ParcelaService = parcelaService;
        }

        // GET api/parcela
        [HttpGet]
        public IActionResult Get()
        {
            var itens = this.ParcelaService.ObterTodos();

            return Ok(itens);
        }
    }
}