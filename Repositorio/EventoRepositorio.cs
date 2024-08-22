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
        public List<EventoModel> BuscarTodos()
        {
            return bancoContext1.Eventos.Where(x => !x.StatusExc).ToList();
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

        public bool Apagar(int id, int id_usuario)
        {
            EventoModel eventoDb = ListarPorId(id);
            if (eventoDb == null) throw new System.Exception("Houve um erro na exclusão do evento do evento!");

            var query = bancoContext1.Eventos.Where(x => x.UsuarioId == id_usuario).FirstOrDefault();
            if (query == null) throw new System.Exception("Usuário que está tentando excluir o evendo não é quem criou o mesmo!");

            if(query!= null)
            {
                eventoDb.StatusExc = true;
                bancoContext1.Eventos.Update(eventoDb);
                bancoContext1.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        
        }
    }
}

