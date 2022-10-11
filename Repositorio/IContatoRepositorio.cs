using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel BuscarPorLogin(string login);
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
        
    }
}
