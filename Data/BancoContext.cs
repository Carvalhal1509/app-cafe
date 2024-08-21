using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;


namespace app_cadastro.Data
{
    public class BancoContext : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
		}

		public DbSet<Usuarios> Usuarios { get; set; }
		
		public DbSet<Arquivos> Arquivos { get; set; }
		
		public DbSet<SugestaoModel> Sugestao { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ 
			base.OnModelCreating(modelBuilder);
		}

	}
}


