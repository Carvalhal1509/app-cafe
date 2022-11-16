using app_cadastro.Data;
using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace app_cadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext bancoContext1;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }

        public Usuarios BuscarPorLogin(string email)
        {
            return bancoContext1.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email);
        }
        public Usuarios ListarPorId(int id)
        {
            return bancoContext1.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public List<Usuarios> BuscarTodos()
        {
            return bancoContext1.Usuarios.Where(x => !x.StatusExc).ToList();
        }

        public List<Usuarios> ListarTodos()
        {
            return bancoContext1.Usuarios.ToList();
        }


        public Usuarios Adicionar(Usuarios contato)
        {
            bancoContext1.Usuarios.Add(contato);
            bancoContext1.SaveChanges();
            return contato;
        }

        public Usuarios Atualizar(Usuarios contato)
        {
            Usuarios contatoDb = ListarPorId(contato.Id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na atualizacão dos dados do usuário!");
            contatoDb.Nome = contato.Nome;
            contatoDb.Senha = contato.Senha;
            contatoDb.Email = contato.Email;
            contatoDb.Aniversario = contato.Aniversario;
            contatoDb.Celular = contato.Celular;
            contatoDb.Perfil = contato.Perfil;

            bancoContext1.Usuarios.Update(contatoDb);
            bancoContext1.SaveChanges();
            return contatoDb;
        }

        public bool Apagar(int id)
        {

            Usuarios contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão dos dados do usuário!");
            contatoDb.StatusExc = true;


            bancoContext1.Usuarios.Update(contatoDb);
            bancoContext1.SaveChanges();

            return true;

        }

            public Usuarios BuscarPorEmailAlterarSenha(string email, string novaSenha)
        {
            var query = bancoContext1.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
            if (query != null)
            {
                query.Senha = novaSenha;

                bancoContext1.Usuarios.Update(query);
                bancoContext1.SaveChanges();
            }

            return query;
        }

        public bool ReativarUsuario(int id)
        {

            Usuarios contatoDb = ListarPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na reativação do usuário!");
            contatoDb.StatusExc = false;


            bancoContext1.Usuarios.Update(contatoDb);
            bancoContext1.SaveChanges();

            return true;

        }
    }
}