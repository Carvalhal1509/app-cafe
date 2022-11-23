using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Infraestrutura
{
    public class ArquivoContext :DbContext
    {
        public ArquivoContext (DbContextOptions<ArquivoContext> options) : base(options)
        {
            //
        }
        public DbSet<Arquivos> Arquivos { get; set; }
    }
}
