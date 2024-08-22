using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    [Table("Vaquinha_Cafe")]
    public class VaquinhaCafeModel
    {
        [Key]
        public int Id_Vaquinha_Cafe { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Chave_Pix { get; set; }

        public DateTime Prazo_Pagamento { get; set; }

        public bool StatusExc { get; set; }
    }
}
