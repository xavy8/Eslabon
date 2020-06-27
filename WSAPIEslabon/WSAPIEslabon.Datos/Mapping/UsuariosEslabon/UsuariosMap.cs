using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WSAPIEslabon.Entidades.UsuariosEslabon;

namespace WSAPIEslabon.Datos.Mapping.UsuariosEslabon
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("UsuariosEslabon")
                    .HasKey(ue => ue.Id);
        }
    }
}
