using app_cadastro.Models;
using System.Collections.Generic;
using System.Linq;
using app_cadastro.Data;

namespace app_cadastro.Repositorio
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly BancoContext bancoContext1;
        public EventoRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }
        
        public EventoModel ListarPorId(int id)
        {
            return bancoContext1.Eventos.FirstOrDefault(x => x.Id == id);
        }
        public List<EventoModel> BuscarTodos(int usuarioId)
        {
            return bancoContext1.Eventos.Where(x => x.UsuarioId == usuarioId && !x.StatusExc).ToList();
        }

       
        public EventoModel Adicionar(EventoModel evento)
        {
            bancoContext1.Eventos.Add(evento);
            bancoContext1.SaveChanges();
            return evento;
        }

        public EventoModel Atualizar(EventoModel evento)
        {

            EventoModel eventoDb = ListarPorId(evento.Id);
            if (eventoDb == null) throw new System.Exception("Houve um erro na atualizacão do evento do usuário!");
            eventoDb.NomeEvento = evento.NomeEvento;
            eventoDb.DataEvento = evento.DataEvento;
            

            bancoContext1.Eventos.Update(eventoDb);
            bancoContext1.SaveChanges();
            return eventoDb;
        }

        public bool Apagar(int id)
        {
            EventoModel eventoDb = ListarPorId(id);
            if (eventoDb == null) throw new System.Exception("Houve um erro na exclusão do evento do usuário!");
            eventoDb.StatusExc = true;


            bancoContext1.Eventos.Update(eventoDb);
            bancoContext1.SaveChanges();

            return true;
        }
    }
}

