using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    [Table("Eventos")]
    public class EventoModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Organizador")]
        public string Organizador { get; set; }

        [Required(ErrorMessage = "Digite o Nome do evento")]
        public string NomeEvento { get; set; }

        public string Descricao { get; set; }

        public DateTime DataEvento { get; set; }

        public bool StatusExc { get; set; }

        [Column("UsuarioId")]
        public int? UsuarioId { get; set; }

    }
}
   
