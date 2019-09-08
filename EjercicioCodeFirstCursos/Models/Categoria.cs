using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioCodeFirstCursos.Models
{
	public class Categoria
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CodCategoria { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage="Es necesario introducir una descripción")]
		public string Descripcion { get; set; }
		public byte[] Imagen { get; set; }
		public string ExtensionImagen { get; set;}
		[Required(ErrorMessage = "Es necesario registrar la fecha de alta")]
		public DateTime Fecha { get; set; }

		public virtual ICollection<Curso> Cursos { get; set;}
	}
}
