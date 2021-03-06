using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Interfaces.Services;

namespace src.Api.Application.Controllers
{
    [ApiController]
    [Route("video-locadora/[controller]")]
    public class ItensAlugueisController : ControllerBase
    {
        private IItemAluguelService _service;

        public ItensAlugueisController(IItemAluguelService service)
        {
            _service = service;            
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetAllItensByAluguelId/{aluguelId}", Name = "GET-ITENS-ALUGUEL-ID")]
        public async Task<IActionResult> GetAllItensByAluguelId(Guid aluguelId)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.GetAllItensByAluguelIdAsync(aluguelId);                
                return Ok(result);
            }
            catch (ArgumentException e)
            {
               return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]        
        public async Task<IActionResult> Post ([FromBody] ItemAluguelDto itemAluguel)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PostItemAluguelAsync(itemAluguel);
                if(result == null) return BadRequest();
                return Created(new Uri(Url.Link("GET-ITENS-ALUGUEL-ID", 
                               new {aluguelId = result.AluguelId})),result);
            }
            catch (ArgumentException e)
            {
               return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]        
        public async Task<IActionResult> Put ([FromBody] ItemAluguelDtoUpdate itemAluguel)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PutItemAluguelAsync(itemAluguel);
                if(result == null) return BadRequest();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
               return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]        
        public async Task<IActionResult> Delete (Guid id)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.DeleteItemAluguelAsync(id);
                if(!result) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
               return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}