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
    public class ProdutoAPIController : ApiController
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: api/ProdutoAPI
        public IQueryable<Produto> GetProdutoes()
        {
            return db.Produtoes;
        }

        // GET: api/ProdutoAPI/5
        [ResponseType(typeof(Produto))]
        public IHttpActionResult GetProduto(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/ProdutoAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduto(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/ProdutoAPI
        [ResponseType(typeof(Produto))]
        public IHttpActionResult PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produtoes.Add(produto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = produto.ProdutoId }, produto);
        }

        // DELETE: api/ProdutoAPI/5
        [ResponseType(typeof(Produto))]
        public IHttpActionResult DeleteProduto(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtoes.Remove(produto);
            db.SaveChanges();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produtoes.Count(e => e.ProdutoId == id) > 0;
        }
    }
}