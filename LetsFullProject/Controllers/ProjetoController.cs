using AutoMapper;
using LetsFullProject.Data;
using LetsFullProject.DTO;
using LetsFullProject.Interfaces;
using LetsFullProject.Models;
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
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IMapper mapper)
        {
            _projetoService = projetoService;
            _mapper = mapper;
        }

        // POST api/projeto
        /// <summary>
        /// Cria projeto na Base
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST api/projeto
        ///     { 
        ///        "nome": "React",
        ///        "URL": "www.react.com.br",
        ///        "Imagem": "",
        ///        "IdUsuarioCadastro": "1"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Projeto inserido na base</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="400">Se o item não for criado</response>   

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProjetoDto> Post([FromBody] ProjetoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new Projeto
            {
                Nome = value.Nome,
                URL = value.URL,
                Imagem = value.Imagem,
                IdUsuarioCadastro = value.IdUsuarioCadastro,
                LikeContador = 0,

            };

            var registryUser = _projetoService.SaveOrUpdate(usuario);
            if (registryUser != null)
            {
                return Ok(registryUser);
            }
            else
            {
                object res = null;
                NotFoundObjectResult notfound = new NotFoundObjectResult(res);
                notfound.StatusCode = 400;
                notfound.Value = "Erro ao cadastrar Usuário!";
                return NotFound(notfound);
            }

        }

        // PATCH api/projeto
        /// <summary>
        /// Da like no projeto vinculando a USUARIOLIKEPROJETO e acrescentando +1 no contador
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PATCH api/projeto
        ///     { 
        ///        "idUsuarioLikeProjeto": "1",
        ///        "idProjetoLikeUsuario": "1",
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Like inserido na base</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="400">Se o item não for criado</response>  
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Projeto> Patch([FromBody] UsuarioLikeProjetoDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioLikeProjeto = new UsuarioLikeProjeto
            {
                IdProjetoLike = value.IdProjetoLike,
                IdUsuarioLike = value.IdUsuarioLike,

            };

            var registryUser = _projetoService.LikeProjeto(usuarioLikeProjeto);

            if (registryUser > 0)
            {
                return Ok(registryUser);
            }
            else
            {
                object res = null;
                NotFoundObjectResult notfound = new NotFoundObjectResult(res);
                notfound.StatusCode = 400;
                notfound.Value = "Erro ao cadastrar Usuário!";
                return NotFound(notfound);
            }
        }

        // GET api/projeto
        /// <summary>
        /// Retorna todos os projetos cadastrados
        /// </summary>        
        /// <response code="200">Retorna a Lista de Projeto</response>
        /// <response code="400">Se não encontrar nenhum resultado</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IList<Usuario>> Get()
        {
            var projetos = _projetoService.GetAll();
            if (projetos != null)
            {
                return Ok(projetos.Select(x => _mapper.Map<Projeto>(x)).ToList());
            }
            else
                return NotFound();
        }
    }
}
