using app_cadastro.Data.Mape;
using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;


namespace app_cadastro.Data


{
    public class BancoContext	 : DbContext
	{
		public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
		}

		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<EventoModel> Eventos { get; set; }
		public DbSet<CafeModel> Cafe { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ 
			modelBuilder.ApplyConfiguration(new ContatoMap());
			base.OnModelCreating(modelBuilder);
		}

	}
}


