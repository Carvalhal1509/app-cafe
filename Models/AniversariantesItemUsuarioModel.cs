using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    [Table("Aniversariantes_Item_Usuario")]
    public class AniversariantesItemUsuarioModel
    {
        public int Id { get; set; }
        public int Id_Aniversariantes { get; set; }
        public int Id_Item { get; set; }
        public string Nome_Item { get; set; }
        public int Id_Usuario { get; set; }
        public string Nome_Usuario { get; set; }
        public bool StatusExc { get; set; }
    }
}
   
