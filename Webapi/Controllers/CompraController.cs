using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webapi.Domain.Services.Abstract;
using Webapi.Entities;
using Webapi.Exceptions;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private const string MensagemErroServidor = "Ocorreu um problema no servidor";
        private const string MensagemDeletadoComSucesso = "Deletado com sucesso";
        private ICompraService CompraService {get; set; }
        public CompraController(ICompraService compraService) 
        {
            this.CompraService = compraService;
        }

        // GET api/compra
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var itens = await this.CompraService.ObterTodos();

                return Ok(itens);

            }catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErroServidor);
            }         
        }

        // GET api/compra/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0){
                return NotFound();
            }

            try{         
                var item =  await this.CompraService.BuscarPorId(id);

                if(item == null)
                {
                    return NotFound();
                }
                return Ok(item);

            }catch(Exception){
               return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um problema no servidor");
            }
        }

        // POST api/compra
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Compra item)
        {
            if(item == null){
                return BadRequest();
            }

            try{

                await this.CompraService.Adicionar(item);
            
                return Created($"api/compra/{item.Id}", item);
            }catch(CompraException e)
            {
                return BadRequest(e.Message);
            }catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErroServidor);
            } 
        }

        // DELETE api/compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             if(id <= 0){
                return NotFound();
            }
            try{

                await this.CompraService.Remover(id);

                return Ok(MensagemDeletadoComSucesso);
            }catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErroServidor);
            }
        }
    }
}
