using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SistemaLoja.Models;

namespace SistemaLoja.Controllers
{
    public class CategoriaAPIController : ApiController
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: api/CategoriaAPI
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET: api/CategoriaAPI/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategoria(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
           

            return Ok(categoria);
        }

        // PUT: api/CategoriaAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CategoriaAPI
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.CategoriaId }, categoria);
        }

        // DELETE: api/CategoriaAPI/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.CategoriaId == id) > 0;
        }
    }
}