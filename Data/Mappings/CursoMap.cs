using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Models;

namespace Tools.Data.Mappings
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Categoria)
                .IsRequired()
                .HasColumnName("Categoria")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Situacao);
            
            builder.Property(x => x.CodigoCRC)
                .IsRequired()
                .HasColumnName("CodigoCRC")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.TurmaCRC)
                .IsRequired()
                .HasColumnName("TurmaCRC")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);
                

                builder.HasMany(x => x.Instrutores)
                .WithMany(x => x.Cursos)
                .UsingEntity<Dictionary<string, object>>(
                    "CursoInstrutor",
                    instrutor => instrutor.HasOne<Instrutor>()
                        .WithMany()
                        .HasForeignKey("InstrutorId")
                        .HasConstraintName("FK_CursoInstrutor_InstrutorId")
                        .OnDelete(DeleteBehavior.NoAction),
                    curso => curso.HasOne<Curso>()
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .HasConstraintName("FK_PCursoInstrutor_CursoId")
                        .OnDelete(DeleteBehavior.NoAction));
        }
    }
}