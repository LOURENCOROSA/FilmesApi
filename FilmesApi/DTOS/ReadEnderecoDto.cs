using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOS;

public class ReadEnderecoDto
{
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
}
