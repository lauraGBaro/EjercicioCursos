using Microsoft.EntityFrameworkCore;

namespace EjercicioCodeFirstCursos.Models
{
	public class CursosContext : DbContext
	{
		public CursosContext(DbContextOptions<CursosContext> options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Curso>()
				.HasOne(nav => nav.Categoria1)
				.WithMany(c => c.Cursos)
				.HasForeignKey(fk => fk.CodCategoria);
		}

		public DbSet<Curso> Cursos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
	}
}
