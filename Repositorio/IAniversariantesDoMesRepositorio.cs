using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface IAniversariantesDoMesRepositorio
    {
        AniversariantesModel ListarPorId(int id);
        ItemAniversarioModel ListarItemPorId(int id);
        List<AniversariantesModel> ListarTodos();
        List<ItemAniversarioModel> ListarTodosItens();
        List<AniversariantesItemUsuarioModel> ListarTodosVinculos(int ide_aniversario);
        AniversariantesModel Adicionar(AniversariantesModel aniversariantes);
        ItemAniversarioModel AdicionarItem(ItemAniversarioModel item);
        AniversariantesItemUsuarioModel AdicionarEscolha(AniversariantesItemUsuarioModel item);
        AniversariantesModel Atualizar(AniversariantesModel aniversariantes);
        bool Apagar(int id);
        bool ApagarItem(int id);
    }
}
