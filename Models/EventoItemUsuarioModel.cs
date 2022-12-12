using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    [Table("Evento_Item_Usuario")]
    public class EventoItemUsuarioModel
    {
        public int Id { get; set; }
        public int Id_Evento { get; set; }
        public int Id_Item_Evento { get; set; }
        public string Nome_Item { get; set; }
        public int Id_Usuario { get; set; }
        public string Nome_Usuario { get; set; }
        public bool StatusExc { get; set; }
    }
}
