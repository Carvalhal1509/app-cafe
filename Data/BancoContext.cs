using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Data

	
{
	public class ControleDeContatos	 : DbContext
	{
		public ControleDeContatos(DbContextOptions<BancoContext> options) : base(options)
       {
		}

		public DbSet<ContatoModel> ContatosTeste { get; set; }
	}
}
