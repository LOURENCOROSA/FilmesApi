using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOS;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório")]
    public string nome { get; set; }
}
