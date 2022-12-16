using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface ICafeRepositorio
    {

        VaquinhaCafeModel ListarPorId(int id);
        List<VaquinhaCafeModel> BuscarTodos();
        VaquinhaCafeModel Adicionar(VaquinhaCafeModel cafe);
        VaquinhaCafeModel Atualizar(VaquinhaCafeModel cafe);
        bool Apagar(int id);

    }
}
