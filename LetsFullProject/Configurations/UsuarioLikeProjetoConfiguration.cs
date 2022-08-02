using LetsFullProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Configurations
{
    public class UsuarioLikeProjetoConfiguration : IEntityTypeConfiguration<UsuarioLikeProjeto>
    {      

        public void Configure(EntityTypeBuilder<UsuarioLikeProjeto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.UsuarioLike)
            .WithMany(b => b.UsuarioLikeProjeto)
            .HasForeignKey(p => p.IdUsuarioLike);

            builder.HasOne(p => p.ProjetoLike)
            .WithMany(b => b.ProjetoLikeUsuario)
            .HasForeignKey(p => p.IdProjetoLike);
        }
    }
}
