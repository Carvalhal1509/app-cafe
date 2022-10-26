using app_cadastro.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Util;

namespace app_cadastro.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite a Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Digite o Email")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Celular")]
        public decimal? Celular { get; set; }

        public DateTime Aniversario { get; set; }

        [Required(ErrorMessage = "Selecione o Perfil")]
        public PerfilEnum Perfil { get; set; }

        public bool StatusExc { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

    }
}