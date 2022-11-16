using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface ICafeRepositorio
    {

        CafeModel ListarPorId(int id);
        List<CafeModel> BuscarTodos();
        CafeModel Adicionar(CafeModel cafe);
        CafeModel Atualizar(CafeModel cafe);
        bool Apagar(int id);

    }
}
