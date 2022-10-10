using app_user.Repositorio;
using ControleDeContatos.Data;
using System.Collections.Generic;
using System.Linq;
using app_usuario.Models;




namespace app_cadastro.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext bancoContext1;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            bancoContext1 = bancoContext;

        }
        public UsuarioModel ListarPorId(int id)
        {
            return bancoContext1.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public List<UsuarioModel>BuscarTodos()
        {
            return bancoContext1.Usuarios.Where(x => !x.StatusExc).ToList();
        }
       
        
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            bancoContext1.Usuarios.Add(usuario);
            bancoContext1.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);
            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualizacão dos dados do usuário!");
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.Email = usuario.Email;

            bancoContext1.Usuarios.Update(usuarioDB);
            bancoContext1.SaveChanges();
            return usuarioDB;
        }

        public bool Apagar(int id)
        {

            UsuarioModel usuarioDB = ListarPorId(id);
            if (usuarioDB == null) throw new System.Exception("Houve um erro na exclusão dos dados do usuário!");
            usuarioDB.StatusExc = true;

           

            bancoContext1.Usuarios.Update(usuarioDB);
            bancoContext1.SaveChanges();
            
            return true;

        }


    }
}
