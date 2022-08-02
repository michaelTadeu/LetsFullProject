using AutoMapper;
using LetsFullProject.DTO;
using LetsFullProject.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")] // TODO add a policy Criada na Controller de forma Global, irá atingir todas as reqs desta controller

    //[Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public LoginController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> Post([FromBody] LoginDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var findUser = _usuarioService.FindByUsername(value.Username);

            if (findUser == null)
                return NotFound();

            var verify = _usuarioService.VerifyPassword(value.Senha, findUser.Id);

            return Ok(verify);

        }
    }
}
