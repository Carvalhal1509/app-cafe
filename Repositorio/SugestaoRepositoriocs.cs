using app_cadastro.Data;
using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Repositorio
{
    public class SugestaoRepositoriocs : ISugestaoRepositoriocs
    {
        private readonly BancoContext bancoContext1;
        public SugestaoRepositoriocs(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }

        public SugestaoModel ListarPorId(int id)
        {
            return bancoContext1.Sugestao.FirstOrDefault(x => x.Id == id);
        }
        public List<SugestaoModel> BuscarTodos()
        {
            return bancoContext1.Sugestao.Where(x => !x.StatusExc).ToList();
        }

        public SugestaoModel Adicionar(SugestaoModel sugestao)
        {
            bancoContext1.Sugestao.Add(sugestao);
            bancoContext1.SaveChanges();
            return sugestao;
        }

     }

 }
 