using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.Login;
using src.Api.Domain.Interfaces.Services;

namespace src.Api.Application.Controllers
{
    [ApiController]
    [Route("video-locadora/[controller]")]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto,
                                        [FromServices] ILoginService loginService)
        {
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                var result = await loginService.FindByLogin(loginDto);
                if(result == null) return NotFound("Usuário não encontrado ou senha incorreta.");
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }            
        }
    }
}