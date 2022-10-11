using app_cadastro.Models;
using ControleDeContatos;
using System;
using ControleDeContatos.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext bancoContext1;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }
        public ContatoModel ListarPorId(int id)
        {
            return bancoContext1.ContatosTeste.FirstOrDefault(x => x.Id == id);
        }
        public List<ContatoModel>BuscarTodos()
        {
            return bancoContext1.ContatosTeste.Where(x => !x.StatusExc).ToList();
        }
       
        
        public ContatoModel Adicionar(ContatoModel contato)
        {
            bancoContext1.ContatosTeste.Add(contato);
            bancoContext1.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = ListarPorId(contato.Id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na atualizacão dos dados do usuário!");
            contatoDb.Nome = contato.Nome;
            contatoDb.Senha = contato.Senha;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;
            contatoDb.Perfil = contato.Perfil;

            bancoContext1.ContatosTeste.Update(contatoDb);
            bancoContext1.SaveChanges();
            return contatoDb;
        }

        public bool Apagar(int id)
        {
           
            ContatoModel contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão dos dados do usuário!");
                contatoDb.StatusExc = true;
          

            bancoContext1.ContatosTeste.Update(contatoDb);
            bancoContext1.SaveChanges();
            
            return true;

        }
    }
}
