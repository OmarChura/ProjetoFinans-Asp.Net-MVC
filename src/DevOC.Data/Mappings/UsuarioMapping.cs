using DevOC.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Login)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnType("varchar(200)");

            //1 : 1 => Fornecedor : Endereco
            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Usuario);

            //1 : N => Fornecedor : Produto
            builder.HasMany(f => f.Orcamentos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);

            builder.ToTable("Usuarios");
        }
    }
}
