using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using app_cadastro.Repositorio;
using Microsoft.AspNetCore.Http;
using app_cadastro.Helper;
using app_cadastro.Data;
using app_cadastro.Infraestrutura;

namespace app_cadastro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataBase")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IEventoRepositorio, EventoRepositorio>();
            services.AddScoped<ICafeRepositorio, CafeRepositorio>();
            services.AddScoped<IAniversariantesRepositorio, AniversariantesRepositorio>();
            services.AddScoped<IAniversariantesDoMesRepositorio, AniversariantesDoMesRepositorio>();
            services.AddScoped<ISugestaoRepositoriocs, SugestaoRepositoriocs> ();
            services.AddScoped<ISessao, Sessao>();
            services.AddScoped<IEmail, Email>();
            services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
