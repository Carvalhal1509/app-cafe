using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(ContatoModel contato);
        void RemoveSessaoDoUsuario();
        ContatoModel BuscarSessaoDoUsuario();

    }
}
