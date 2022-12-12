using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    [Table("Item_Evento")]
    public class ItemEventoModel
    {
        public int Id { get; set; }
        public int Id_Evento { get; set; }
        public string Nome { get; set; }
        public bool StatusExc { get; set; }

    }
}
