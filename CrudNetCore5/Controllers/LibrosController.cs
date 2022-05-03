using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudNetCore5.Data;
using CrudNetCore5.Models;

namespace CrudNetCore5.Controllers
{
    public class LibrosController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        //http get Index
        public IActionResult Index()
        {

            IEnumerable<Libro> listLibros = _context.Libro;
            return View(listLibros);
        }


        //http get create
        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public IActionResult Create(Libro libro )
        {
            if (ModelState.IsValid) {
                _context.Libro.Add(libro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
             }
            
            //obtengo el libro
            var libro=_context.Libro.Find(id);


            if (libro==null)

            {
                return NotFound();
            }
            return View(libro);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]//valida no bots
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libro.Update(libro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }








        
        public IActionResult Delete(int? id )
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //obtengo el libro
            var libro = _context.Libro.Find(id);


            if (libro == null)

            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpDelete]
        //[HttpPost]
        [ValidateAntiForgeryToken]//valida no bots
        public IActionResult DeleteLibro(int? id) {

            var libro = _context.Libro.Find(id);
            if (libro==null)
            {
                return NotFound();
            }

            _context.Libro.Remove(libro);
            this._context.SaveChanges();
            TempData["mensaje"] = "El libro se ha eliminado correctamente";
            

            return RedirectToAction("Index");
        }

        



    }
}
