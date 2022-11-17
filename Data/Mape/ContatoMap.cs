using app_cadastro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cadastro.Data.Mape
{
    public class ContatoMap : IEntityTypeConfiguration<EventoModel>
    {
        public void Configure(EntityTypeBuilder<EventoModel> builder)
        {
            builder.HasKey(x =>x.Id);
            builder.HasOne(x => x.UsuarioModal);
        }
    }
}
