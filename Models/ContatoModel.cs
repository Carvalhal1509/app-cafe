using app_cadastro.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite o Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Digite a Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage ="Digite o Email")]
        [EmailAddress(ErrorMessage ="O e-mail informado nao é valido!")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Digite o Celular")]
        [Phone(ErrorMessage ="O celular informado não é valido!")]
        public string Celular { get; set; }
        public DateTime Aniversario { get; set; }
        public PerfilEnum Perfil { get; set; }
        public bool StatusExc { get; set; }




    }
}