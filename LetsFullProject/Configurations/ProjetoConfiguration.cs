using LetsFullProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsFullProject.Configurations
{
    public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.UsuarioCadastro)
            .WithMany(b => b.Projeto)
            .HasForeignKey(p => p.IdUsuarioCadastro)
            .HasConstraintName("FK_PROJETO_USUARIO_ID");
        }
    }
}
