using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioCodeFirstCursos.Models
{
	public class DtoCurso
	{
		public int CodCurso { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage = "Es necesario introducir una descripción")]
		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }
		public IFormFile Imagen { get; set; }
		public byte[] ImagenGuardada { get; set; }
		public string ExtensionImagen { get; set; }
		public bool Destacado { get; set; }
		[Required(ErrorMessage = "Es necesario registrar el inicio del curso")]
		[Display(Name = "Fecha de inicio")]
		public DateTime Fecha { get; set; }

		[Required(ErrorMessage = "Es necesario seleccionar una categoría")]
		[Display(Name = "Categoría")]
		public int CodCategoria { get; set; }
		[Display(Name = "Categoría")]
		public string DescCategoria { get; set; }

		public DtoCurso()
		{

		}

		public DtoCurso(Curso curso)
		{
			CodCurso = curso.CodCurso;
			Descripcion = curso.Descripcion;
			ImagenGuardada = curso.Imagen;
			ExtensionImagen = curso.ExtensionImagen;
			CodCategoria = curso.CodCategoria;
			Destacado = curso.Destacado;
			Fecha = curso.Fecha;
			DescCategoria = curso.Categoria1?.Descripcion;
		}
	}
}
