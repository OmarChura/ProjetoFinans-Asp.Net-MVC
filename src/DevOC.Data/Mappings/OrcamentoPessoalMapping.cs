using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOC.Business.Models;

namespace DevOC.Data.Mappings
{
    public class OrcamentoPessoalMapping : IEntityTypeConfiguration<OrcamentoPessoal>
    {
        public void Configure(EntityTypeBuilder<OrcamentoPessoal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Dia)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.TipoDespesa)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder
              .Property<DateTime>(p => (DateTime)p.DataAtualizacao)
              .HasColumnType("datetime")
              .HasDefaultValueSql("getdate()");

            builder.ToTable("Despesas");

        }
        
    }
}
