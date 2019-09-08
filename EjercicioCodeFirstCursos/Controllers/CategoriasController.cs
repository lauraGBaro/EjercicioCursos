using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjercicioCodeFirstCursos.Models;
using System.IO;

namespace EjercicioCodeFirstCursos.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CursosContext _context;

        public CategoriasController(CursosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.Select(m => new DtoCategoria(m)).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.Select(m => new DtoCategoria(m)).FirstOrDefaultAsync(m => m.CodCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DtoCategoria categoria)
        {
			byte[] imagen = null;
			string extensionImagen = null;

			if (categoria.Imagen?.Length > 0)
			{
				using (var memstream = new MemoryStream())
				{
					categoria.Imagen.OpenReadStream().CopyTo(memstream);
					imagen = memstream.ToArray();
					extensionImagen = categoria.Imagen.ContentType;
				}
			}

			if (ModelState.IsValid)
            {
				var newDbItem = new Categoria
				{
					CodCategoria = categoria.CodCategoria,
					Descripcion = categoria.Descripcion,
					ExtensionImagen = extensionImagen,
					Imagen = imagen,
					Fecha = categoria.Fecha
				};

                _context.Add(newDbItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.Select(m => new DtoCategoria(m)).FirstOrDefaultAsync(m => id == m.CodCategoria);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DtoCategoria categoria)
        {
			var categoriaDB = await _context.Categorias.FirstOrDefaultAsync(m => categoria.CodCategoria == m.CodCategoria);

			categoriaDB.CodCategoria = categoria.CodCategoria;
			categoriaDB.Descripcion = categoria.Descripcion;
			categoriaDB.Fecha = categoria.Fecha;

			if (categoria.Imagen?.Length > 0)
			{
				using (var memstream = new MemoryStream())
				{
					categoria.Imagen.OpenReadStream().CopyTo(memstream);
					categoriaDB.Imagen = memstream.ToArray();
					categoriaDB.ExtensionImagen = categoria.Imagen.ContentType;
				}
			}

			if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoriaDB.CodCategoria))
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
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.Select(m => new DtoCategoria(m)).FirstOrDefaultAsync(m => m.CodCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CodCategoria == id);
        }

		public async Task<FileContentResult> GetImg(int id)
		{
			var categoria = await _context.Categorias.FindAsync(id);
			if (categoria != null)
			{
				return File(categoria.Imagen, categoria.ExtensionImagen);
			}
			else
			{
				return null;
			}
		}
	}
}
