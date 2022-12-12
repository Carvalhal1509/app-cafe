using app_cadastro.Data;
using app_cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace app_cadastro.Repositorio
{
    public class AniversariantesRepositorio : IAniversariantesRepositorio
    {
        private readonly BancoContext bancoContext1;
        public AniversariantesRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }

        public List<Usuarios> BuscarTodos()
        {
            return bancoContext1.Usuarios.Where(x => !x.StatusExc && x.Aniversario.Month == DateTime.Now.Month).ToList();
        }

        public List<Usuarios> ListarTodos()
        {
            return bancoContext1.Usuarios.ToList();
        }

       
    }
}