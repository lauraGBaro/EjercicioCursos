using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjercicioCodeFirstCursos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace EjercicioCodeFirstCursos.Controllers
{
    public class CursosController : Controller
    {
        private readonly CursosContext _context;

        public CursosController(CursosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos
				.Include(i => i.Categoria1)
				.Select(m => new DtoCurso(m))
				.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.Select(m => new DtoCurso(m)).FirstOrDefaultAsync(m => m.CodCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public async Task<IActionResult> Create()
        {
			ViewBag.CodCategoria = new SelectList(await _context.Categorias.ToListAsync(), nameof(Categoria.CodCategoria),nameof(Categoria.Descripcion));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DtoCurso curso)
        {
			byte[] imagen = null;
			string extensionImagen = null;

			if(curso.Imagen?.Length > 0)
			{
				using (var memstream = new MemoryStream())
				{
					curso.Imagen.OpenReadStream().CopyTo(memstream);
					imagen = memstream.ToArray();
					extensionImagen = curso.Imagen.ContentType;
				}
			}

            if (ModelState.IsValid)
            {
				var newDbItem = new Curso
				{
					CodCurso = curso.CodCurso,
					Descripcion = curso.Descripcion,
					Imagen = imagen,
					ExtensionImagen = extensionImagen,
					CodCategoria = curso.CodCategoria,
					Destacado = curso.Destacado,
					Fecha = curso.Fecha
				};

                _context.Add(newDbItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			
			var curso = await _context.Cursos.Select(m => new DtoCurso(m)).FirstOrDefaultAsync(m => id == m.CodCurso);
            if (curso == null)
            {
                return NotFound();
            }

			SelectList lista = new SelectList(await _context.Categorias.ToListAsync(), nameof(Categoria.CodCategoria), nameof(Categoria.Descripcion));
			foreach(var item in lista)
			{
				if(item.Value == curso.CodCategoria.ToString())
				{
					item.Selected = true;
				}
			}
			ViewBag.CodCategoria = lista;

			return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DtoCurso curso)
        {
			var cursoDB = await _context.Cursos.FirstOrDefaultAsync(m => curso.CodCurso == m.CodCurso);

			cursoDB.Descripcion = curso.Descripcion;
			cursoDB.CodCategoria = curso.CodCategoria;
			cursoDB.Destacado = curso.Destacado;
			cursoDB.Fecha = curso.Fecha;

			if (curso.Imagen?.Length > 0)
			{
				using (var memstream = new MemoryStream())
				{
					curso.Imagen.OpenReadStream().CopyTo(memstream);
					cursoDB.Imagen = memstream.ToArray();
					cursoDB.ExtensionImagen = curso.Imagen.ContentType;
				}
			}

			if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursoDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(cursoDB.CodCurso))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.Select(m => new DtoCurso(m)).FirstOrDefaultAsync(m => m.CodCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.CodCurso == id);
        }


		public async Task<FileContentResult> GetImg(int id)
		{
			var curso = await _context.Cursos.FindAsync(id);
			if (curso != null)
			{
				return File(curso.Imagen, curso.ExtensionImagen);
			}
			else
			{
				return null;
			}
		}
	}
}
