using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOS;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string nome { get; set; }
}
