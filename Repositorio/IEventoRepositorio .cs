using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_cadastro.Models;

namespace app_cadastro.Repositorio
{
    public interface IEventoRepositorio
    {
        
        EventoModel ListarPorId(int id);
        List<EventoModel> BuscarTodos();
        EventoModel Adicionar(EventoModel evento);
        EventoModel Atualizar(EventoModel evento);
        bool Apagar(int id);

    }
}
