﻿using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTOS;

public class UpdateFilmeDto
{
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "Tamanho maxímo 50 caracteres")]
    public string Genero { get; set; }
    [Required(ErrorMessage = "Insira uma duração")]
    [Range(70, 600, ErrorMessage = "a duração deve ser entre 70 e 600 minutos")]
    public int Duracao { get; set; }
}
