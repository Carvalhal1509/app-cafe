using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace app_login.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite a Senha")]
        public string Senha { get; set; }
    }
}
