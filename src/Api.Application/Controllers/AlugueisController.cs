using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Interfaces.Services;

namespace src.Api.Application.Controllers
{
    [ApiController]
    [Route("video-locadora/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly IAluguelService _service;

        public AlugueisController(IAluguelService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllByUsuarioId/{usuarioId}")]
        public async Task<IActionResult> GetAllByUsuarioId(Guid usuarioId)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.GetAllByUsuarioIdAsync(usuarioId);
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetCompleteById/{aluguelId}", Name = "GET-COMPLETE-ID")]
        public async Task<IActionResult> GetCompleteById(Guid aluguelId)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.GetCompleteByIdAsync(aluguelId);
                if(result == null) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] AluguelDto aluguel)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PostAluguelAsync(aluguel);
                if(result == null) return BadRequest();
                return Created(new Uri(Url.Link("GET-COMPLETE-ID", 
                               new {aluguelId = result.Id})), result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]        
        public async Task<IActionResult> Put ([FromBody] AluguelDtoUpdate aluguel)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PutAluguelAsync(aluguel);
                if(result == null) return BadRequest();
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        } 

        [HttpDelete]
        [Route("{id}")]        
        public async Task<IActionResult> Delete (Guid id)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.DeleteAluguelAsync(id);
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