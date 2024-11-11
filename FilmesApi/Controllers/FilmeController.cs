using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTOS;
using FilmesApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id },
                filme);
        }
    }

    /// <summary>
    /// Busca filmes no banco
    /// </summary>
    /// <param name="skip">Pule para a lista desejada</param>
    /// <param name="take">Selecione a quantidade de objetos por lista</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Filmes encontrados com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery]int skip = 0, [FromQuery]int take = 50)
    { 
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take)); 
    }
    /// <summary>
    /// Busca filmes no banco pelo Id
    /// </summary>
    /// <param name="id">Digite o id do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Filme encontrado com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }
    /// <summary>
    /// Atualiza todos os dados do filme 
    /// </summary>
    /// <param name="id">Digite o id do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Filme atualizado com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody]
    UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault( filme => filme.Id == id);
        if (filme == null) return NotFound(); 
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Atualiza parcialmente os dados do filme 
    /// </summary>
    ///  <remarks>
    /// Exemplo de requisição:
    /// 
    ///     
    ///     {
    ///         "op": "replace",
    ///         "path": "/titulo",
    ///         "value": "titulo desejado"
    ///     }
    /// </remarks>
    /// <param name="id">Digite o id do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Filme atualizado com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmePatch(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Deleta o filme pelo id
    /// </summary>
    /// <param name="id">Digite o id do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Filme deletado com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
