using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_usuario.Models;

namespace app_user.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Apagar(int id);
        
    }
}
