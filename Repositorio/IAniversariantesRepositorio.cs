using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface IAniversariantesRepositorio
    {
        List<Usuarios> BuscarTodos();
        List<Usuarios> ListarTodos();
    }
}
