﻿using System.ComponentModel.DataAnnotations;
namespace app_cadastro.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }
    }
}
