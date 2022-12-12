using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Models
{
    public class Arquivos
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
        public int Id_Usuario { get; set; }
        public string NomeUsuario { get; set; }
        public int Id_Cafe { get; set; }
        public bool StatusAprovado { get; set; }
        public DateTime Data_Envio { get; set; } = DateTime.Now;
    }
}
