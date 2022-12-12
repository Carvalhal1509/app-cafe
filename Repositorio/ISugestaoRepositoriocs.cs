using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Repositorio
{
    public interface ISugestaoRepositoriocs
    {
        public SugestaoModel ListarPorId(int id);
        public List<SugestaoModel> BuscarTodos();
        public SugestaoModel Adicionar(SugestaoModel sugestao);
    }
}
