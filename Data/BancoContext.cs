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
		public DbSet<EventoModel> Eventos { get; set; }
		public DbSet<CafeModel> Cafe { get; set; }
		public DbSet<AniversariantesModel> AniversariantesDoMes { get; set; }
		public DbSet<Arquivos> Arquivos { get; set; }
		public DbSet<ItemAniversarioModel> ItemAniversario { get; set; }
		public DbSet<AniversariantesItemUsuarioModel> AniversariantesItemUsuario { get; set; }
		public DbSet<ItemEventoModel> ItemEvento { get; set; }
		public DbSet<EventoItemUsuarioModel> EventoItemUsuario { get; set; }
		public DbSet<SugestaoModel> Sugestao { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ 
			base.OnModelCreating(modelBuilder);
		}

	}
}


