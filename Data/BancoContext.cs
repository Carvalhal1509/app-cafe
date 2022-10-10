using app_cadastro.Models;
using app_usuario.Models;
using ControleDeContatos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_usuario;

namespace ControledeBanco.Data


{
	public class ControleDeContatos : DbContext
	{
		public ControleDeContatos(DbContextOptions<BancoContext> options) :
			base(options)
		{ 
		}
		public DbSet<ContatoModel> ContatosTeste { get; set; }
		public DbSet<UsuarioModel> Usuarios { get; set; }
	     
	}
}
	

