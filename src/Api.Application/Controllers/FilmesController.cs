using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;

namespace src.Api.Application.Controllers
{
    [ApiController]
    [Route("video-locadora/[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _service;
        public FilmesController(IFilmeService service)
        {
            _service = service;            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.GetAllAsync();
                if(result.Count() == 0) return NotFound("Lista Vazia");
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);                
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GET-FILME-ID")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.GetAsync(id);
                if(result == null) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmeDto filme)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PostAsync(filme);
                if(result == null) return BadRequest();
                return Created(new Uri(Url.Link("GET-FILME-ID", 
                               new {Id = result.Id})), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);                
            }
        }

        [HttpPut]        
        public async Task<IActionResult> Put([FromBody] FilmeDtoUpdate filme)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.PutAsync(filme);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var result = await _service.DeleteAsync(id);
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