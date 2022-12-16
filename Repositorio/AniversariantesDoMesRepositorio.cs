using app_cadastro.Data;
using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace app_cadastro.Repositorio
{
    public class AniversariantesDoMesRepositorio : IAniversariantesDoMesRepositorio
    {
        private readonly BancoContext bancoContext1;
        public AniversariantesDoMesRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;
        }

        public AniversariantesModel ListarPorId(int id)
        {
            return bancoContext1.AniversariantesDoMes.FirstOrDefault(x => x.Id == id);
        }

        public ItemAniversarioModel ListarItemPorId(int id)
        {
            return bancoContext1.ItemAniversario.FirstOrDefault(x => x.Id == id);
        }

        public List<AniversariantesModel> ListarTodos()
        {
            return bancoContext1.AniversariantesDoMes.ToList();
        }

        public List<ItemAniversarioModel> ListarTodosItens()
        {
            return bancoContext1.ItemAniversario.ToList();
        }

        public List<AniversariantesItemUsuarioModel> ListarTodosVinculos(int ide_aniversario)
        {
            return bancoContext1.AniversariantesItemUsuario.Where(x => x.Id_Aniversariantes == ide_aniversario && !x.StatusExc).ToList();
        }


        public AniversariantesModel Adicionar(AniversariantesModel aniversariantes)
        {
            bancoContext1.AniversariantesDoMes.Add(aniversariantes);
            bancoContext1.SaveChanges();
            return aniversariantes;
        }

        public ItemAniversarioModel AdicionarItem(ItemAniversarioModel item)
        {
            bancoContext1.ItemAniversario.Add(item);
            bancoContext1.SaveChanges();
            return item;
        }

        public AniversariantesItemUsuarioModel AdicionarEscolha(AniversariantesItemUsuarioModel item)
        {
            var query = ListarItemPorId(item.Id_Item);
            query.StatusExc = true;          
            bancoContext1.AniversariantesItemUsuario.Add(item);
            bancoContext1.ItemAniversario.Update(query);
            bancoContext1.SaveChanges();
            return item;
        }

        public AniversariantesModel Atualizar(AniversariantesModel contato)
        {
            AniversariantesModel contatoDb = ListarPorId(contato.Id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na atualizacão da lista de aniversariantes!");
            contatoDb.Nome = contato.Nome;
            contatoDb.Descricao = contato.Descricao;
            contatoDb.DataRealizacao = contato.DataRealizacao;

            bancoContext1.AniversariantesDoMes.Update(contatoDb);
            bancoContext1.SaveChanges();
            return contatoDb;
        }

        public bool Apagar(int id)
        {

            AniversariantesModel contatoDb = ListarPorId(id);
            var query = ListarTodosItens().Where(x => x.StatusExc == true).ToList();
            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão da lista de aniversariantes");
            contatoDb.StatusExc = true;

            for (int i = 0; i < query.Count; i++)
            {
                query[i].StatusExc = false;
                bancoContext1.ItemAniversario.Update(query[i]);
            }

            bancoContext1.AniversariantesDoMes.Update(contatoDb);

            bancoContext1.SaveChanges();

            return true;

        }


        public bool ApagarItem(int id)
        {

            ItemAniversarioModel contatoDb = ListarItemPorId(id);
            if (contatoDb == null) throw new System.Exception("Houve um erro na exclusão do item!");

            bancoContext1.ItemAniversario.Remove(contatoDb);
            bancoContext1.SaveChanges();

            return true;

        }

    }
}