using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTOS;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmesApi.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas aos cinemas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        /// <summary>
        /// Construtor do controlador CinemaController.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        /// <param name="mapper">Mapper para conversão de DTOs.</param>
        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um novo cinema ao banco de dados.
        /// </summary>
        /// <param name="cinemaDto">Objeto DTO com os dados do cinema a ser criado.</param>
        /// <returns>Retorna a ação criada com o ID do cinema.</returns>
        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaDto);
        }

        /// <summary>
        /// Recupera a lista de cinemas, podendo ser filtrada pelo ID do endereço.
        /// </summary>
        /// <param name="enderecoId">ID opcional do endereço para filtrar os cinemas.</param>
        /// <returns>Retorna uma lista de DTOs dos cinemas encontrados.</returns>
        [HttpGet]
        public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
        {
            if (enderecoId == null)
            {
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            }
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas where Cinemas.enderecoId = {enderecoId}").ToList());
        }

        /// <summary>
        /// Recupera um cinema pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cinema a ser recuperado.</param>
        /// <returns>Retorna o DTO do cinema encontrado ou NotFound se não for encontrado.</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um cinema existente.
        /// </summary>
        /// <param name="id">ID do cinema a ser atualizado.</param>
        /// <param name="cinemaDto">Objeto DTO com os novos dados do cinema.</param>
        /// <returns>Retorna NoContent se a atualização for bem-sucedida ou NotFound se o cinema não existir.</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um cinema pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cinema a ser deletado.</param>
        /// <returns>Retorna NoContent se a exclusão for bem-sucedida ou NotFound se o cinema não existir.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
