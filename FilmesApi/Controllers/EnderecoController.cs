using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTOS;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas aos endereços.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        /// <summary>
        /// Construtor do controlador EnderecoController.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        /// <param name="mapper">Mapper para conversão de DTOs.</param>
        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um novo endereço ao banco de dados.
        /// </summary>
        /// <param name="enderecoDto">Objeto DTO com os dados do endereço a ser criado.</param>
        /// <returns>Retorna a ação criada com o ID do endereço.</returns>
        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }

        /// <summary>
        /// Recupera a lista de todos os endereços.
        /// </summary>
        /// <returns>Retorna uma lista de DTOs dos endereços encontrados.</returns>
        [HttpGet]
        public IEnumerable<ReadEnderecoDto> RecuperaEnderecos()
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos);
        }

        /// <summary>
        /// Recupera um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser recuperado.</param>
        /// <returns>Retorna o DTO do endereço encontrado ou NotFound se não for encontrado.</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return Ok(enderecoDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um endereço existente.
        /// </summary>
        /// <param name="id">ID do endereço a ser atualizado.</param>
        /// <param name="enderecoDto">Objeto DTO com os novos dados do endereço.</param>
        /// <returns>Retorna NoContent se a atualização for bem-sucedida ou NotFound se o endereço não existir.</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um endereço pelo seu ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser deletado.</param>
        /// <returns>Retorna NoContent se a exclusão for bem-sucedida ou NotFound se o endereço não existir.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
