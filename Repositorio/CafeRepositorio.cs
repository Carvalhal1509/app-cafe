using app_cadastro.Models;
using System.Collections.Generic;
using System.Linq;
using app_cadastro.Data;

namespace app_cadastro.Repositorio
{
    public class CafeRepositorio : ICafeRepositorio
    {
        private readonly BancoContext bancoContext1;
        public CafeRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }
        
      public CafeModel Adicionar(CafeModel cafe)
        {
            bancoContext1.Cafe.Add(cafe);
            bancoContext1.SaveChanges();
            return cafe;
        }
        


        public CafeModel Atualizar(CafeModel cafee)
        {
            {

                CafeModel cafeDb = ListarPorId(cafee.Id);
                if (cafeDb == null) throw new System.Exception("Houve um erro na atualizacão do cafe do usuário!");
                cafeDb.Cafe = cafee.Cafe;
                cafeDb.DataCafe = cafee.DataCafe;


                bancoContext1.Cafe.Update(cafeDb);
                bancoContext1.SaveChanges();
                return cafeDb;
            }
        }
        public bool Apagar(int id)
        {
           CafeModel cafeDb = ListarPorId(id);
            if (cafeDb == null) throw new System.Exception("Houve um erro na exclusão do cafe do usuário!");
            cafeDb.StatusExc = true;


            bancoContext1.Cafe.Update(cafeDb);
            bancoContext1.SaveChanges();

            return true;
        }

        public CafeModel ListarPorId(int id)
        {
            return bancoContext1.Cafe.FirstOrDefault(x => x.Id == id);
        }

        public List<CafeModel> BuscarTodos()
        {
            return bancoContext1.Cafe.Where(x => !x.StatusExc).ToList();
        }
    }
}

