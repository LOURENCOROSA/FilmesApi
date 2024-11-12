using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOS;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório")]
    public string nome { get; set; }
}
