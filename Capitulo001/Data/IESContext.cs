﻿using Capitulo001.Models;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using Modelo.Discente;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Capitulo001.Models.Infra;
using Modelo.Docente;

namespace Capitulo001.Data
{
    public class IESContext : IdentityDbContext<UsuarioDaAplicacao>
    {
  

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico>Academicos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("Departamento");
            modelBuilder.Entity<CursoDisciplina>()
                .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursosDisciplinas)
                .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(d => d.Disciplina)
                .WithMany(cd => cd.CursosDisciplinas)
                .HasForeignKey(d => d.DisciplinaID);

            modelBuilder.Entity<CursoProfessor>()
                .HasKey(cd => new { cd.CursoID, cd.ProfessorID });

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursosProfessores)
                .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(d => d.Professor)
                .WithMany(cd => cd.CursosProfessores)
                .HasForeignKey(d => d.ProfessorID);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=INT_CDU_0-83\\SQLEXPRESS\\SQLEXPRESS;DataBase=localdb;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
    }
}

