using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using FilmesApi.DTOS;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas às sessões.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        /// <summary>
        /// Construtor do controlador SessaoController.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        /// <param name="mapper">Mapper para conversão de DTOs.</param>
        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma nova sessão ao banco de dados.
        /// </summary>
        /// <param name="dto">Objeto DTO com os dados da sessão a ser criada.</param>
        /// <returns>Retorna a ação criada com o ID do filme e do cinema da sessão.</returns>
        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto dto)
        {
            Sessao sessao = _mapper.Map<Sessao>(dto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
        }

        /// <summary>
        /// Recupera a lista de todas as sessões.
        /// </summary>
        /// <returns>Retorna uma lista de DTOs das sessões encontradas.</returns>
        [HttpGet]
        public IEnumerable<ReadSessaoDto> RecuperaSessoes()
        {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Recupera uma sessão específica pelo ID do filme e ID do cinema.
        /// </summary>
        /// <param name="filmeId">ID do filme da sessão a ser recuperada.</param>
        /// <param name="cinemaId">ID do cinema da sessão a ser recuperada.</param>
        /// <returns>Retorna o DTO da sessão encontrada ou NotFound se não for encontrada.</returns>
        [HttpGet("{filmeId}/{cinemaId}")]
        public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
            if (sessao != null)
            {
                ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
                return Ok(sessaoDto);
            }
            return NotFound();
        }
    }
}
