using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioCodeFirstCursos.Models
{
	public class Curso
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CodCurso { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage = "Es necesario introducir una descripción")]
		public string Descripcion { get; set; }
		public byte[] Imagen { get; set; }
		public string ExtensionImagen { get; set;}
		public bool Destacado { get; set; }
		[Required(ErrorMessage = "Es necesario registrar el inicio del curso")]
		public DateTime Fecha { get; set; }

		[ForeignKey("Categoria")]
		[Required(ErrorMessage = "Es necesario seleccionar una categoría")]
		public int CodCategoria { get; set; }

		public virtual Categoria Categoria1 { get; set; }
	}
}
