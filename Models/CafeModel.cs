using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    public class CafeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Organizador")]
        public string Organizador { get; set; }

        [Required(ErrorMessage = "Digite o Nome do Cafe")]
        public string Cafe { get; set; }

        public DateTime DataCafe { get; set; }

        public bool StatusExc { get; set; }
    }
}
