using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioCodeFirstCursos.Models
{
	public class DtoCategoria
	{
		public int CodCategoria { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage = "Es necesario introducir una descripción")]
		[Display (Name = "Descripción")]
		public string Descripcion { get; set; }
		public IFormFile Imagen { get; set; }
		public string ExtensionImagen { get; set; }
		public byte[] ImagenGuardada { get; set; }
		[Required(ErrorMessage = "Es necesario registrar la fecha de alta")]
		[Display(Name = "Fecha de alta")]
		public DateTime Fecha { get; set; }

		public DtoCategoria()
		{

		}

		public DtoCategoria(Categoria categoria)
		{
			CodCategoria = categoria.CodCategoria;
			Descripcion = categoria.Descripcion;
			ImagenGuardada = categoria.Imagen;
			ExtensionImagen = categoria.ExtensionImagen;
			Fecha = categoria.Fecha;

		}
	}
}
