using EjercicioCodeFirstCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioCodeFirstCursos.Views.Shared.Components.Destacado
{
	public class DestacadoViewComponent : ViewComponent
	{
		private readonly CursosContext db;

		public DestacadoViewComponent(CursosContext context)
		{
			db = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = await GetItemsAsync();
			return View(items);
		}

		private Task<List<Curso>> GetItemsAsync()
		{
			return db.Cursos.Where(c => c.Destacado).ToListAsync();
		}
	}
}
