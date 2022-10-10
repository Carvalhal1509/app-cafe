using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_usuario.Models;

namespace ControleDeContatos.Data
{
	public class BancoContext : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
		}

		public DbSet<ContatoModel> ContatosTeste { get; set; }
		public DbSet<UsuarioModel> Usuarios { get; set; }

	}
}
