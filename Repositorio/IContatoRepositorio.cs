using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        Usuarios BuscarPorLogin(string email);
        Usuarios BuscarPorEmailAlterarSenha(string email, string novaSenha);
        Usuarios ListarPorId(int id);
        List<Usuarios> BuscarTodos();
        List<Usuarios> ListarTodos();
        Usuarios Adicionar(Usuarios contato);
        Usuarios Atualizar(Usuarios contato);
        bool Apagar(int id);
        bool ReativarUsuario(int id);

    }
}
