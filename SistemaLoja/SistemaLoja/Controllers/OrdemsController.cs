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
    public class OrdemsController : ApiController
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: api/Ordems
        public IHttpActionResult GetOrdem()
        {
            var ordens = db.Ordem.ToList();
            var ordensAPI = new List<OrdensAPI>();
            foreach(var ordem in ordens)
            {
                var ordemAPI = new OrdensAPI
                {
                    customizar = ordem.Customizar,
                    OrdemData = ordem.OrdemData,
                    Detalhes = ordem.OrdensDetalhes,
                    OrdemsId = ordem.OrdemId,
                    OrdemStatus = ordem.OrdemStatus
                };
                ordensAPI.Add(ordemAPI);
            }


            return Ok(ordensAPI);
        }

        // GET: api/Ordems/5
        [ResponseType(typeof(Ordem))]
        public IHttpActionResult GetOrdem(int id)
        {
            Ordem ordem = db.Ordem.Find(id);
            if (ordem == null)
            {
                return NotFound();
            }

            return Ok(ordem);
        }

        // PUT: api/Ordems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdem(int id, Ordem ordem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordem.OrdemId)
            {
                return BadRequest();
            }

            db.Entry(ordem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemExists(id))
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

        // POST: api/Ordems
        [ResponseType(typeof(Ordem))]
        public IHttpActionResult PostOrdem(Ordem ordem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ordem.Add(ordem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ordem.OrdemId }, ordem);
        }

        // DELETE: api/Ordems/5
        [ResponseType(typeof(Ordem))]
        public IHttpActionResult DeleteOrdem(int id)
        {
            Ordem ordem = db.Ordem.Find(id);
            if (ordem == null)
            {
                return NotFound();
            }

            db.Ordem.Remove(ordem);
            db.SaveChanges();

            return Ok(ordem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdemExists(int id)
        {
            return db.Ordem.Count(e => e.OrdemId == id) > 0;
        }
    }
}