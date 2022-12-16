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
        
      public VaquinhaCafeModel Adicionar(VaquinhaCafeModel cafe)
        {
            bancoContext1.Cafe.Add(cafe);
            bancoContext1.SaveChanges();
            return cafe;
        }
        


        public VaquinhaCafeModel Atualizar(VaquinhaCafeModel cafee)
        {
            {

                VaquinhaCafeModel cafeDb = ListarPorId(cafee.Id_Vaquinha_Cafe);
                if (cafeDb == null) throw new System.Exception("Houve um erro na atualizacão da vaquinha!");
                cafeDb.Nome = cafee.Nome;
                cafeDb.Descricao = cafee.Descricao;
                cafeDb.Chave_Pix = cafee.Chave_Pix;
                cafeDb.Prazo_Pagamento = cafee.Prazo_Pagamento;


                bancoContext1.Cafe.Update(cafeDb);
                bancoContext1.SaveChanges();
                return cafeDb;
            }
        }
        public bool Apagar(int id)
        {
           VaquinhaCafeModel cafeDb = ListarPorId(id);
            if (cafeDb == null) throw new System.Exception("Houve um erro na exclusão da vaquinha!");
            cafeDb.StatusExc = true;


            bancoContext1.Cafe.Update(cafeDb);
            bancoContext1.SaveChanges();

            return true;
        }

        public VaquinhaCafeModel ListarPorId(int id)
        {
            return bancoContext1.Cafe.FirstOrDefault(x => x.Id_Vaquinha_Cafe == id);
        }

        public List<VaquinhaCafeModel> BuscarTodos()
        {
            return bancoContext1.Cafe.Where(x => !x.StatusExc).ToList();
        }
    }
}

